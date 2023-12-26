using System;
using System.Collections;
using Sources.Audio;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    [RequireComponent(typeof(TargetSeeker))]
    public sealed class Shooter : MonoBehaviour, IAudioPlayable
    {
        [SerializeField] [Min(0.001f)] private float _fireRate;
        [SerializeField] private bool _isShooting;
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shotMaker;

        private Coroutine _shootingCoroutine;

        private TargetSeeker _targetSeeker;
        private WaitForSeconds _waitForShot;

        public event Action AudioPlaying;

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

        private IShotMaker ShotMaker => (IShotMaker)_shotMaker;

        private void Awake()
        {
            _targetSeeker = GetComponent<TargetSeeker>();
            FireRate = _fireRate;
        }

        private void OnEnable()
        {
            _targetSeeker.TargetFounded += StartShoot;
            _targetSeeker.TargetLosted += StopShoot;

            if (_isShooting)
            {
                StartShoot();
            }
        }

        private void OnDisable()
        {
            _targetSeeker.TargetFounded -= StartShoot;
            _targetSeeker.TargetLosted -= StopShoot;

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
                AudioPlaying?.Invoke();

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