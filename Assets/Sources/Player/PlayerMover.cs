using Sources.Input;
using Sources.Tools;
using Sources.Tools.Extensions;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Player
{
    public sealed class PlayerMover : MonoBehaviour, ICarControllable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Car _vehicle;
        [SerializeField] private float _rotationSpeed = 1;
        [SerializeField] private float _angleEpsilon = 0.0001f;
        [SerializeField] private float _moveSpeed;

        private Vector2 _prevDirection;
        private Vector2 _inputDirection;

        public Vector2 ForwardDirection => new Vector2(transform.forward.x, transform.forward.z).normalized;
        public float CurrentSpeed => _rigidbody.velocity.magnitude;

        private void Start()
        {
            Move(ForwardDirection);
        }

        private void FixedUpdate()
        {
            Rotate();
            SetVelocity();
        }

        private void OnDrawGizmos()
        {
            Vector3 position = _rigidbody.position;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, new Vector3(ForwardDirection.x, position.y, ForwardDirection.y) * 6f);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(position, new Vector3(_inputDirection.x, position.y, _inputDirection.y) * 6f);
        }

        public void Move(Vector2 newDirection)
        {
            if (Mathf.Approximately(newDirection.magnitude, 0f))
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

        private void RotateToDirection(Vector2 direction)
        {
            _inputDirection = direction;
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