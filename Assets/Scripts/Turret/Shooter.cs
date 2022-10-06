using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace Turret
{
    [RequireComponent(typeof(TargetSeeker))]
    [RequireComponent(typeof(ProjectilePool))]
    public sealed class Shooter : MonoBehaviour
    {
        [SerializeField] private bool _isShooting;
        [SerializeField] private Transform[] _shootPoints;
        [SerializeField] [Min(0.001f)] private float _cooldown;

        private TargetSeeker _targetSeeker;
        private ProjectilePool _pool;
        private WaitForSeconds _waitForShot;
        private Coroutine _shoot;

        public event Action<int> ShotOff;

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
            _pool = GetComponent<ProjectilePool>();
            _targetSeeker = GetComponent<TargetSeeker>();
            Cooldown = _cooldown;
        }

        private void Start()
        {
            if (_isShooting)
            {
                StartShoot();
            }
        }

        private void OnEnable()
        {
            _targetSeeker.TargetFound += StartShoot;
            _targetSeeker.TargetLost += StopShoot;
        }

        private void OnDisable()
        {
            _targetSeeker.TargetFound -= StartShoot;
            _targetSeeker.TargetLost -= StopShoot;
        }

#if UNITY_EDITOR
        [ContextMenu("CallStartShoot")]
        public void CallStartShoot()
        {
            StartShoot();
        }

        [ContextMenu("CallStopShoot")]
        public void CallStopShoot()
        {
            StopShoot();
        }
#endif

        private IEnumerator Shoot()
        {
            while (_isShooting)
            {
                for (int i = 0; i < _shootPoints.Length; i++)
                {
                    _pool.Enable(_shootPoints[i].position, _shootPoints[i].rotation);
                    ShotOff?.Invoke(i);
                    yield return _waitForShot;
                }
            }
        }

        private void StopShoot()
        {
            if (_shoot == null)
            {
                return;
            }

            StopCoroutine(_shoot);
            _shoot = null;
            _isShooting = false;
        }

        private void StartShoot()
        {
            if (_shoot != null)
            {
                return;
            }

            _isShooting = true;
            _shoot = StartCoroutine(Shoot());
        }
    }
}