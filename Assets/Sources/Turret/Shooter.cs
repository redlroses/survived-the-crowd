using System;
using System.Collections;
using UnityEngine;

namespace Sources.Turret
{
    [RequireComponent(typeof(TargetSeeker))]
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shotMaker;
        [SerializeField] private bool _isShooting;
        [SerializeField] [Min(0.001f)] private float _cooldown;

        private TargetSeeker _targetSeeker;
        private WaitForSeconds _waitForShot;
        private Coroutine _shootingCoroutine;

        private IShotMaker ShotMaker => (IShotMaker) _shotMaker;

        public float Cooldown
        {
            get => _cooldown;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Cooldown));
                }

                _cooldown = value;
                _waitForShot = new WaitForSeconds(value);
            }
        }

        private void Awake()
        {
            _targetSeeker = GetComponent<TargetSeeker>();
            Cooldown = _cooldown;
        }

        private void OnEnable()
        {
            _targetSeeker.TargetFound += StartShoot;
            _targetSeeker.TargetLost += StopShoot;

            if (_isShooting)
            {
                StartShoot();
            }
        }

        private void OnDisable()
        {
            _targetSeeker.TargetFound -= StartShoot;
            _targetSeeker.TargetLost -= StopShoot;

            StopShoot();
        }

        private IEnumerator Shoot()
        {
            while (_isShooting)
            {
                ShotMaker.MakeShot();
                yield return _waitForShot;
            }
        }

        [ContextMenu("CallStopShoot")]
        private void StopShoot()
        {
            if (_shootingCoroutine == null)
            {
                return;
            }

            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
            _isShooting = false;
        }

        [ContextMenu("CallStartShoot")]
        private void StartShoot()
        {
            _isShooting = true;
            _shootingCoroutine ??= StartCoroutine(Shoot());
        }
    }
}