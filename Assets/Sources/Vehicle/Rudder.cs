using System;
using Sources.StaticData;
using Sources.Tools.Extensions;
using Sources.Vehicle.View;
using UnityEngine;
using static Sources.Tools.Constants;

namespace Sources.Vehicle
{
    public sealed class Rudder : MonoBehaviour
    {
        [SerializeField] private Transform _backAxle;
        [SerializeField] private Transform _leftWheel;
        [SerializeField] [Range(0, 90f)] private float _maxAngle = 30f;
        [SerializeField] private Transform _rightWheel;
        [SerializeField] private WheelRotator _wheelRotator;

        private float _backAxleTurningRadius;
        private float _frontAxleLength;
        private RotationDirection _rotationDirection;
        private Vector3 _turningPoint;
        private Vector3 _vehicleCenter;
        private float _wheelAngle;
        private float _wheelBase;

        public Vector3 MoveDirection { get; private set; }

        public Vector3 WheelDirection { get; private set; }

        private void Start()
        {
            _wheelBase = GetWheelBase();
            _frontAxleLength = GetFrontAxleLength();
        }

        private void OnEnable()
        {
            MoveDirection = Vector3.left;
            WheelDirection = Vector3.left;
        }

        public void Construct(CarStaticData carStaticData)
        {
            _maxAngle = carStaticData.MaxWheelAngle;
        }

        public void DeflectSteeringWheel(float angle)
        {
            _wheelAngle = angle.Clamp(_maxAngle);
            _rotationDirection = (RotationDirection)Mathf.Sign(angle);
            _vehicleCenter = GetVehicleCenter();

            CalculateMoveDirection();
            RotateWheels();
        }

        private float GetWheelBase()
            => Mathf.Abs(_leftWheel.localPosition.z - _backAxle.localPosition.z);

        private float GetFrontAxleLength()
            => Mathf.Abs(_leftWheel.localPosition.x) + Mathf.Abs(_rightWheel.localPosition.x);

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
            WheelDirection = GetDirectionInPosition(GetNearWheelToTurnPoint().position);
            MoveDirection = GetDirectionInPosition(_vehicleCenter);
        }

        private float GetTurningRadius()
        {
            float wheelAngleTan = Mathf.Tan((HalfPiDegrees - _wheelAngle) * Mathf.Deg2Rad);

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
            Vector3 radiusVector = GetCentralRadiusVector(position);

            Vector3 normalizedMoveDirection =
                Vector3.Cross(radiusVector, Vector3.up * (int)_rotationDirection).normalized;

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
                _ => throw new ArgumentOutOfRangeException(),
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
            float farWheelTurningRadius = _backAxleTurningRadius + _frontAxleLength * (int)_rotationDirection;
            float farWheelAngle = HalfPiDegrees - Mathf.Atan2(farWheelTurningRadius, _wheelBase) * Mathf.Rad2Deg;

            return farWheelAngle;
        }
    }
}