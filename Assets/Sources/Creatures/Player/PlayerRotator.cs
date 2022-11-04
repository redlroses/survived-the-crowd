using System;
using Sources.Vehicle;
using Tools;
using Tools.Extensions;
using UnityEngine;

namespace Sources.Creatures.Player
{
    public sealed class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _deviationAngle;
        [SerializeField] private Car _vehicle;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private float _rotationSpeed = 1;

        private Quaternion _rotation;

        private void FixedUpdate()
        {
            _vehicle.Rudder.DeflectSteeringWheel(_deviationAngle);
            Quaternion targetRotation = Quaternion.LookRotation(_vehicle.Rudder.WheelDirection);
            _rigidbody.rotation =
                Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);
        }

        private void EmulateRotating()
        {
            _vehicle.Rudder.DeflectSteeringWheel(_deviationAngle);
        }

        private float CalculateInputAngle(Vector2 direction, float dotProduct)
        {
            float dotSign = Mathf.Sign(dotProduct);
            return Mathf.Atan2(direction.x * dotSign, direction.y * dotSign) * Mathf.Rad2Deg;
        }

        private float ClampAngle(float angle)
        {
            if (angle > Constants.PiDegrees)
            {
                angle -= Constants.TwoPiDegrees;
            }

            if (angle < -Constants.PiDegrees)
            {
                angle += Constants.TwoPiDegrees;
            }

            return angle;
        }
    }
}
