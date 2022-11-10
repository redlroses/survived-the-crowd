using System;
using Sources.Vehicle.View;
using Tools.Extensions;
using Types;
using UnityEngine;
using static Tools.Constants;
using static UnityEngine.Gizmos;
using static UnityEngine.Mathf;

namespace Sources.Vehicle
{
    public sealed class Rudder : MonoBehaviour
    {
        private readonly float _debugSphereRadius = 0.2f;
        private readonly float _moveDirectionGizmosLength = 6f;

        [SerializeField] [Range(0, 90f)] private float _maxAngle = 30f;
        [SerializeField] private Transform _leftWheel;
        [SerializeField] private Transform _rightWheel;
        [SerializeField] private Transform _backAxle;
        [SerializeField] private WheelRotator _wheelRotator;

        private float _wheelBase;
        private float _wheelAngle;
        private float _frontAxleLength;
        private float _backAxleTurningRadius;
        private Vector3 _turningPoint;
        private Vector3 _vehicleCenter;
        private Vector3 _wheelDirection;
        private Vector3 _moveDirection;
        private RotationDirection _rotationDirection;

        public Vector3 MoveDirection => _moveDirection;
        public Vector3 WheelDirection => _wheelDirection;

        private void Start()
        {
            _wheelBase = GetWheelBase();
            _frontAxleLength = GetFrontAxleLength();
            DeflectSteeringWheel(0.1f);
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

        private float GetWheelBase()
            => Abs(_leftWheel.localPosition.z - _backAxle.localPosition.z);

        private float GetFrontAxleLength()
            => Abs(_leftWheel.localPosition.x) + Abs(_rightWheel.localPosition.x);

        private Vector3 GetVehicleCenter()
        {
            Vector3 center = new Vector3(
                _leftWheel.localPosition.x + _frontAxleLength * ToHalf,
                _leftWheel.localPosition.y,
                _leftWheel.localPosition.z - _wheelBase * ToHalf);

            center = transform.TransformPoint(center);
            return center;
        }

        private void CalculateMoveDirection()
        {
            _backAxleTurningRadius = GetTurningRadius();
            _turningPoint = GetTurningPoint();
            _wheelDirection = GetDirectionInPosition(GetNearWheelToTurnPoint().position);
            _moveDirection = GetDirectionInPosition(_vehicleCenter);
        }

        private float GetTurningRadius()
        {
            float wheelAngleTan = Tan((HalfPiDegrees - _wheelAngle) * Deg2Rad);
            return _wheelBase * wheelAngleTan;
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

        private Vector3 GetDirectionInPosition(Vector3 position)
        {
            Vector3 radiusVector = GetCentralRadiusVector(toPoint: position);
            Vector3 normalizedMoveDirection = Vector3.Cross(radiusVector, Vector3.up * (int) _rotationDirection).normalized;
            return normalizedMoveDirection;
        }

        private Vector3 GetCentralRadiusVector(Vector3 toPoint)
            => _turningPoint - toPoint;

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
                    _wheelRotator.RotateWheels(_wheelAngle, GetFarWheelAngle());
                    break;
                case RotationDirection.Right:
                    _wheelRotator.RotateWheels(GetFarWheelAngle(), _wheelAngle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float GetFarWheelAngle()
        {
            float farWheelTurningRadius = _backAxleTurningRadius + _frontAxleLength * (int) _rotationDirection;
            float farWheelAngle = HalfPiDegrees - Atan2(farWheelTurningRadius, _wheelBase) * Rad2Deg;
            return farWheelAngle;
        }
    }
}