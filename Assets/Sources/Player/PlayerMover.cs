using Sources.Input;
using Sources.Tools;
using Sources.Tools.Extensions;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Player
{
    public sealed class PlayerMover : MonoBehaviour, ICarControllable
    {
        [SerializeField] private float _angleEpsilon = 0.0001f;
        [SerializeField] private float _rotationSpeed = 1;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Car _vehicle;

        private bool _isInitialized;
        private Vector2 _prevDirection;

        private Vector2 ForwardDirection => new Vector2(transform.forward.x, transform.forward.z).normalized;

        private float CurrentSpeed => _rigidbody.velocity.magnitude;

        private void Reset()
        {
            _rigidbody.velocity = Vector3.zero;
            _prevDirection = Vector2.left;
        }

        private void FixedUpdate()
        {
            if (_isInitialized == false)
            {
                return;
            }

            Rotate();
            SetVelocity();
        }

        private void OnEnable()
        {
            Reset();
        }

        public void Move(Vector2 newDirection)
        {
            if (Mathf.Approximately(newDirection.sqrMagnitude, 0f))
            {
                RotateToDirection(_prevDirection);
            }
            else
            {
                RotateToDirection(newDirection);
                _prevDirection = _vehicle.Rudder.WheelDirection.ToInputFormat();
            }
        }

        public void Accelerate()
        {
            _vehicle.Engine.BeginAcceleration();
        }

        public void Decelerate()
        {
            _vehicle.Engine.StopAcceleration();
        }

        public void Init(Car car)
        {
            _vehicle = car;
            _isInitialized = true;
        }

        private void RotateToDirection(Vector2 direction)
        {
            float moveAngle = Mathf.Atan2(ForwardDirection.x, ForwardDirection.y) * Mathf.Rad2Deg;
            float inputAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angleDeviation = moveAngle - inputAngle;
            float deviationAngle = -ClampAngle(angleDeviation);

            if (Mathf.Abs(deviationAngle) <= _angleEpsilon)
            {
                return;
            }

            _vehicle.Rudder.DeflectSteeringWheel(deviationAngle);
        }

        private void Rotate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_vehicle.Rudder.MoveDirection);

            _rigidbody.rotation =
                Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);
        }

        private void SetVelocity()
        {
            _moveSpeed = _vehicle.Engine.CalculateNewSpeed(CurrentSpeed);

            if (_moveSpeed <= 0)
            {
                return;
            }

            Vector3 moveDirection = _vehicle.Rudder.MoveDirection;
            Vector3 velocity = moveDirection * _moveSpeed;
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