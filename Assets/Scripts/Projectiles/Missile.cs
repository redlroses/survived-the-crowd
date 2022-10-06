using Turret;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Rotator))]
    [RequireComponent(typeof(TargetSeeker))]
    public sealed class Missile : Projectile
    {
        [SerializeField] private Rotator _rotator;
        [SerializeField] private TargetSeeker _targetSeeker;

        private Transform _target;

        private void Awake()
        {
            if (_rotator == null)
            {
                _rotator = GetComponent<Rotator>();
            }

            if (_targetSeeker == null)
            {
                _targetSeeker = GetComponent<TargetSeeker>();
            }
        }

        private void OnEnable()
        {
            _targetSeeker.TargetUpdated += OnTargetUpdated;
        }

        private void OnDisable()
        {
            _targetSeeker.TargetUpdated -= OnTargetUpdated;
            _target = null;
        }

        private void OnTargetUpdated(Transform target)
        {
            _target = target;
        }

        protected override void Move(float moveSpeed)
        {
            if (_target && _target.gameObject.activeSelf)
            {
                RotateToTarget();
            }

            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }

        private void RotateToTarget()
        {
            var rotationToTarget = Quaternion.LookRotation(_target.position - transform.position);
            _rotator.Rotate(rotationToTarget);
        }
    }
}
