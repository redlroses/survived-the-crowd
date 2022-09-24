using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Turret
{
    public sealed class TargetSeeker : MonoBehaviour
    {
        private readonly int _cacheSize = 30;

        [SerializeField] private bool _isScanning;
        [SerializeField] [Min(0)] private float _scanRadius;
        [SerializeField] [Min(0.001f)] private float _scanFrequency;
        [SerializeField] private LayerMask _filter;

        private Coroutine _scan;
        private WaitForSeconds _waitForScan;
        private Collider[] _cachedTargets;
        private Transform _closestTarget;
        private bool _isHasTarget;

        public event Action<Transform> TargetUpdated;
        public event Action TargetLost;
        public event Action TargetFound;

        private float ScanFrequency
        {
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(_scanFrequency));
                }

                _scanFrequency = value;
                _waitForScan = new WaitForSeconds(1f / value);
            }
        }

        private void Awake()
        {
            ScanFrequency = _scanFrequency;
            _cachedTargets = new Collider[_cacheSize];
        }

        private void Start()
        {
            if (_isScanning)
            {
                StartScan();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _scanRadius);
        }

#if UNITY_EDITOR
        [ContextMenu("CallStartScan")]
        public void CallStartShoot()
        {
            StartScan();
        }

        [ContextMenu("CallStopScan")]
        public void CallStopShoot()
        {
            StopScan();
        }
#endif

        private IEnumerator Scan()
        {
            while (_isScanning)
            {
                int targetsCount = FindTargets();
                InvokeStateEvents(targetsCount);

                if (targetsCount == 0)
                {
                    yield return _waitForScan;

                    continue;
                }

                TryUpdateTarget(GetClosest());

                if (targetsCount > _cacheSize)
                {
                    _cachedTargets = new Collider[_cachedTargets.Length + _cacheSize];
                }

                Debug.Log("Scan - Closest " + _closestTarget.name);
                yield return _waitForScan;
            }
        }

        private int FindTargets()
        {
            ClearCache();
            return Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, _cachedTargets, _filter);
        }

        private void TryUpdateTarget(Transform newClosest)
        {
            if (newClosest == _closestTarget)
            {
                return;
            }

            _closestTarget = newClosest;
            TargetUpdated?.Invoke(_closestTarget);
        }

        private void InvokeStateEvents(int targetsCount)
        {
            if (targetsCount == 0)
            {
                if (_isHasTarget)
                {
                    TargetLost?.Invoke();
                    _isHasTarget = false;
                }
            }
            else
            {
                if (_isHasTarget == false)
                {
                    TargetFound?.Invoke();
                    _isHasTarget = true;
                }
            }
        }

        private Transform GetClosest()
        {
            var minDistance = float.MaxValue;
            Transform closest = null;

            var filtredtargets = _cachedTargets
                .Where(target => target != null)
                .Select(target => target.transform);

            foreach (var target in filtredtargets)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget >= minDistance)
                {
                    continue;
                }

                minDistance = distanceToTarget;
                closest = target;
            }

            if (closest is null)
            {
                throw new NullReferenceException();
            }

            return closest;
        }

        private void ClearCache()
        {
            for (var i = 0; i < _cacheSize; i++)
            {
                _cachedTargets[i] = null;
            }
        }

        private void StopScan()
        {
            if (_scan == null)
            {
                return;
            }

            StopCoroutine(_scan);
            _scan = null;
            _isScanning = false;
        }

        private void StartScan()
        {
            if (_scan != null)
            {
                return;
            }

            _isScanning = true;
            _scan = StartCoroutine(Scan());
        }
    }
}