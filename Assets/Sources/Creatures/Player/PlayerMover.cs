using System;
using Sources.Input;
using Sources.Tools.Extensions;
using Sources.Vehicle;
using Tools;
using UnityEngine;

namespace Sources.Creatures.Player
{
    public sealed class PlayerMover : MonoBehaviour, ICarControllable
    {
        private readonly float _angleEpsilon = 0.00001f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Car _vehicle;
        [SerializeField] private float _deviationAngle;
        [SerializeField] private float _rotationSpeed = 1;

        private float _moveSpeed;
        private float _lastMoveSpeed;

        private Vector2 _prevDirection;
        private Vector2 _inputDirection;

        public Vector2 ForwardDirection => new Vector2(transform.forward.x, transform.forward.z).normalized;

        private void FixedUpdate()
        {
            Rotate();
            SetVelocity();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(_rigidbody.position, new Vector3(ForwardDirection.x, _rigidbody.position.y, ForwardDirection.y) * 6f);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_rigidbody.position, new Vector3(_inputDirection.x, _rigidbody.position.y, _inputDirection.y) * 6f);
        }

        public void Move(Vector2 newDirection)
        {
            if (Mathf.Approximately(newDirection.magnitude, 0f))
            {
                MoveToDirection(_prevDirection);
            }
            else
            {
                MoveToDirection(newDirection);
                _prevDirection = _vehicle.Rudder.WheelDirection.ToInputFormat();
            }
        }

        public void Accelerate()
        {
            _vehicle.Engine.BeginAcceleration();
        }

        public void Decelerate()
        {
            _vehicle.Engine.BeginDeceleration();
        }

        private void MoveToDirection(Vector2 direction)
        {
            _inputDirection = direction;
            float moveAngle = Mathf.Atan2(ForwardDirection.x, ForwardDirection.y) * Mathf.Rad2Deg;
            float inputAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angleDeviation = moveAngle - inputAngle;
            Debug.Log($"move: {moveAngle}, input: {inputAngle}, deviation: {angleDeviation}");
            _deviationAngle = -ClampAngle(angleDeviation);
            _vehicle.Rudder.DeflectSteeringWheel(_deviationAngle);
        }

        private void Rotate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_vehicle.Rudder.WheelDirection);
            _rigidbody.rotation =
                Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);
        }

        private void SetVelocity()
        {
            _moveSpeed = _vehicle.Engine.CalculateNewSpeed(_lastMoveSpeed);
            _lastMoveSpeed = _moveSpeed;

            if (_moveSpeed <= 0)
            {
                return;
            }

            Vector3 moveDirection = _vehicle.Rudder.WheelDirection;
            var velocity = moveDirection * _moveSpeed;
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
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