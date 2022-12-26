using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Turret
{
    public class Radar : MonoBehaviour
    {
        [SerializeField] private int _cacheSize = 30;
        [SerializeField] private bool _isScanning;
        [SerializeField] [Min(0)] private float _scanRadius;
        [SerializeField] [Min(0.001f)] private float _scanFrequency;
        [SerializeField] private LayerMask _filter;

        private int _targetsCount;
        private Coroutine _scan;
        private WaitForSeconds _waitForScan;
        private Collider[] _cachedTargets;

        public event Action Updated;

        public int TargetsCount => _targetsCount;

        public IEnumerable<Transform> Targets => _cachedTargets
            .Where(target => target != null)
            .Select(target => target.transform);

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

        private void OnDrawGizmos()
        {
            if (_isScanning == false)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _scanRadius);
            Gizmos.color = Color.white;
        }

        [ContextMenu("Stop Scan")]
        protected void StopScan()
        {
            if (_scan == null)
            {
                return;
            }

            StopCoroutine(_scan);
            _scan = null;
            _isScanning = false;
        }

        [ContextMenu("Start Scan")]
        protected void StartScan()
        {
            if (_scan != null)
            {
                return;
            }

            _isScanning = true;
            _scan = StartCoroutine(Scan());
        }

        private IEnumerator Scan()
        {
            while (_isScanning)
            {
                ClearCache(_targetsCount);
                _targetsCount = FindTargets();
                Updated?.Invoke();

                yield return _waitForScan;
            }
        }

        private int FindTargets()
            => Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, _cachedTargets, _filter);

        private void ClearCache(int size)
        {
            for (var i = 0; i < size; i++)
            {
                _cachedTargets[i] = null;
            }
        }
    }
}