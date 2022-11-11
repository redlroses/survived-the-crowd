using System;
using Sources.Pool;
using UnityEngine;

namespace Sources.Turret
{
    [RequireComponent(typeof(ProjectilePool))]
    public sealed class ProjectileShotMaker : MonoBehaviour, IShotMaker
    {
        [SerializeField] private ProjectilePool _pool;
        [SerializeField] private Transform _shotPoint;

        public event Action ShotOff;

        private void Awake()
        {
            _pool ??= GetComponent<ProjectilePool>();
        }

        public void MakeShot()
        {
            _pool.Enable(_shotPoint.position, _shotPoint.rotation);
            ShotOff?.Invoke();
        }
    }
}