using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class EnemyMover : MonoBehaviour
    {
        private readonly Vector3 _rotatingDirection = new Vector3(0.5f, 0.5f, 0.5f);

        [SerializeField] private Transform _target;
        [SerializeField] private float _moveForce = 50f;
        [SerializeField] private float _jumpForce = 30f;
        [SerializeField] private float _rotationForce = 10f;
        [SerializeField] private Vector2 _moveForceMagnitudeRange = new Vector2(10f, 20f);
        [SerializeField] private float _jumpDirectionScatter = 1f;

        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out Ground _))
            {
                Jump();
            }
        }

        public void Init(Transform target)
        {
            _target = target;
        }

        private void Rotate()
        {
            _rigidBody.AddTorque(_rotatingDirection * _rotationForce, ForceMode.Acceleration);
        }

        private void Jump()
        {
            float randomDirectionXComponent = Random.Range(-_jumpDirectionScatter, _jumpDirectionScatter);
            float randomDirectionZComponent = Random.Range(-_jumpDirectionScatter, _jumpDirectionScatter);
            Vector3 randomDirection = new Vector3(randomDirectionXComponent, 0, randomDirectionZComponent);
            _rigidBody.AddForce((Vector3.up + randomDirection).normalized * _jumpForce, ForceMode.VelocityChange);
        }

        private void Move()
        {
            Vector3 direction = _target.position - transform.position;
            float magnitude = Mathf.Clamp(direction.magnitude, _moveForceMagnitudeRange.x, _moveForceMagnitudeRange.y);
            direction = direction.normalized;
            _rigidBody.AddForce(direction * (magnitude * _moveForce), ForceMode.Acceleration);
        }
    }
}