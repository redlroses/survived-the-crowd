using UnityEngine;

namespace Turret
{
    [RequireComponent(typeof(TargetSeeker))]
    public sealed class Aimer : MonoBehaviour
    {
        [SerializeField] private bool _isAiming;
        [SerializeField] private Transform _target;

        private TargetSeeker _targetSeeker;

        private void Awake()
        {
            _targetSeeker = GetComponent<TargetSeeker>();
        }

        private void OnEnable()
        {
            _targetSeeker.TargetUpdated += SetTarget;
            _targetSeeker.TargetLost += StopAim;
            _targetSeeker.TargetFound += StartAim;
        }

        private void Update()
        {
            if (_isAiming)
            {
                transform.LookAt(_target);
            }
        }

        private void OnDisable()
        {
            _targetSeeker.TargetUpdated -= SetTarget;
            _targetSeeker.TargetLost -= StopAim;
            _targetSeeker.TargetFound -= StartAim;
        }

        private void SetTarget(Transform target)
        {
            _target = target;
        }

        private void StopAim()
        {
            _isAiming = false;
        }

        private void StartAim()
        {
            _isAiming = true;
        }
    }
}