using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Turret
{
    public class Radar : MonoBehaviour
    {
        private readonly int _cacheSize = 15;

        [SerializeField] private bool _isScanning;
        [SerializeField] [Min(0)] private float _scanRadius;
        [SerializeField] [Min(0.001f)] private float _scanFrequency;
        [SerializeField] private LayerMask _filter;

        private int _targetsCount;
        private Coroutine _scan;
        private WaitForSeconds _waitForScan;
        private Collider[] _cachedTargets;

        private void Awake()
        {
            ScanFrequency = _scanFrequency;
            _cachedTargets = new Collider[_cacheSize];
        }

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

        protected virtual void OnTargetsUpdated()
        {
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
                OnTargetsUpdated();

                if (_targetsCount == 0)
                {
                    yield return _waitForScan;

                    continue;
                }

                if (_targetsCount > _cacheSize)
                {
                    _cachedTargets = new Collider[_cachedTargets.Length + _cacheSize];
                    continue;
                }

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