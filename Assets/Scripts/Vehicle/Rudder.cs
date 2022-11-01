using System;
using Tools.Extensions;
using Types;
using UnityEngine;
using static Tools.Constants;
using static UnityEngine.Gizmos;
using static UnityEngine.Mathf;

namespace Vehicle
{
    [ExecuteInEditMode]
    public sealed class Rudder : MonoBehaviour
    {
        private readonly float _debugSphereRadius = 0.2f;
        private readonly float _moveDirectionGizmosLength = 6f;

        [SerializeField] [Range(0, 90f)] private float _maxAngle = 30f;
        [SerializeField] private Transform _leftWheel;
        [SerializeField] private Transform _rightWheel;
        [SerializeField] private Transform _backAxle;

        private float _wheelBase;
        private float _wheelAngle;
        private float _frontAxleLength;
        private float _backAxleTurningRadius;
        private Vector3 _turningPoint;
        private Vector3 _vehicleCenter;
        private Vector3 _moveDirection;
        private RotationDirection _rotationDirection;

        public Vector3 MoveDirection => _moveDirection;

        private void Start()
        {
            _wheelBase = GetWheelBase();
            _frontAxleLength = GetFrontAxleLength();
        }

        private void OnDrawGizmos()
        {
            color = Color.red;
            DrawRay(_rightWheel.transform.position, -_rightWheel.transform.right * int.MaxValue);
            DrawRay(_rightWheel.transform.position, _rightWheel.transform.right * int.MaxValue);
            DrawRay(_leftWheel.transform.position, _leftWheel.transform.right * int.MaxValue);
            DrawRay(_leftWheel.transform.position, -_leftWheel.transform.right * int.MaxValue);
            color = Color.magenta;
            DrawRay(_backAxle.transform.position, _backAxle.transform.right * int.MaxValue);
            DrawRay(_backAxle.transform.position, -_backAxle.transform.right * int.MaxValue);
            color = Color.green;
            DrawSphere(_turningPoint, _debugSphereRadius);
            color = Color.blue;
            DrawSphere(_vehicleCenter, _debugSphereRadius);
            color = Color.yellow;
            DrawRay(transform.position, _moveDirection * _moveDirectionGizmosLength);
        }

        public void DeflectSteeringWheel(float angle)
        {
            _wheelAngle = angle.Clamp(_maxAngle);
            _rotationDirection = (RotationDirection) Sign(angle);
            _vehicleCenter = GetVehicleCenter();

            CalculateMoveDirection();
            RotateWheels();
        }

        private float GetFrontAxleLength()
        {
            return Abs(_leftWheel.localPosition.x) + Abs(_rightWheel.localPosition.x);
        }

        private float GetWheelBase()
        {
            return Abs(_leftWheel.localPosition.z - _backAxle.localPosition.z);
        }

        private Vector3 GetVehicleCenter()
        {
            Vector3 center = new Vector3(_leftWheel.localPosition.x + _frontAxleLength * ToHalf, _leftWheel.localPosition.y, _leftWheel.localPosition.z - _wheelBase * ToHalf);
            center = transform.TransformPoint(center);
            return center;
        }

        private void CalculateMoveDirection()
        {
            _backAxleTurningRadius = GetTurningRadius();
            _turningPoint = GetTurningPoint();
            _moveDirection = GetMoveDirection();
        }

        private Vector3 GetMoveDirection()
        {
            Vector3 centralRadiusVector = _turningPoint - _vehicleCenter;
            Vector3 normalizedMoveDirection = Vector3.Cross(centralRadiusVector, Vector3.up * (int) _rotationDirection).normalized;
            return normalizedMoveDirection;
        }

        private Vector3 GetTurningPoint()
        {
            Transform settlementWheel = GetNearWheelToTurnPoint();
            Vector3 settlementWheelLocalPosition = settlementWheel.localPosition;

            _turningPoint = new Vector3(
                settlementWheelLocalPosition.x + _backAxleTurningRadius,
                settlementWheelLocalPosition.y,
                settlementWheelLocalPosition.z - _wheelBase);

            return transform.TransformPoint(_turningPoint);
        }

        private float GetTurningRadius()
        {
            float wheelAngleTan = Tan((HalfPiDegrees - _wheelAngle) * Deg2Rad);
            return _wheelBase * wheelAngleTan;
        }

        private Transform GetNearWheelToTurnPoint()
        {
            return _rotationDirection switch
            {
                RotationDirection.Left => _leftWheel,
                RotationDirection.Right => _rightWheel,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void RotateWheels()
        {
            switch (_rotationDirection)
            {
                case RotationDirection.Left:
                    SetWheelsAngle(_leftWheel, _rightWheel, _wheelAngle);
                    break;
                case RotationDirection.Right:
                    SetWheelsAngle(_rightWheel, _leftWheel, _wheelAngle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetWheelsAngle(Transform nearWheel, Transform farWheel, float nearWheelAngle)
        {
            float farWheelTurningRadius = _backAxleTurningRadius + _frontAxleLength * (int) _rotationDirection;
            float farWheelAngle = HalfPiDegrees - Atan2(farWheelTurningRadius, _wheelBase) * Rad2Deg;

            nearWheel.SetLocalEulerY(nearWheelAngle);
            farWheel.SetLocalEulerY(farWheelAngle);
        }
    }
}