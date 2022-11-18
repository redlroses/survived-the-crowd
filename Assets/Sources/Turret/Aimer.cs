using UnityEngine;

namespace Sources.Turret
{
    [RequireComponent(typeof(Rotator))]
    [RequireComponent(typeof(TargetSeeker))]
    public sealed class Aimer : MonoBehaviour
    {
        [SerializeField] private TargetSeeker _targetSeeker;
        [SerializeField] private Rotator _rotator;
        [SerializeField] private bool _isAiming;

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
            _targetSeeker.TargetUpdated += SetTarget;
            _targetSeeker.TargetLost += StopAim;
            _targetSeeker.TargetFound += StartAim;
        }

        private void Update()
        {
            if (_isAiming)
            {
                _rotator.RotateTo(_target);
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