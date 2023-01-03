using System;
using System.Collections;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    [RequireComponent(typeof(TargetSeeker))]
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shotMaker;
        [SerializeField] private bool _isShooting;
        [SerializeField] [Min(0.001f)] private float _fireRate;

        private TargetSeeker _targetSeeker;
        private WaitForSeconds _waitForShot;

        private Coroutine _shootingCoroutine;

        private IShotMaker ShotMaker => (IShotMaker) _shotMaker;

        public float FireRate
        {
            get => _fireRate;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(FireRate));
                }

                _fireRate = value;
                _waitForShot = new WaitForSeconds(1f / _fireRate);
            }
        }

        private void Awake()
        {
            _targetSeeker = GetComponent<TargetSeeker>();
            FireRate = _fireRate;
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

        public void Construct(WeaponStaticData weaponData)
        {
            FireRate = weaponData.FireRate;
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