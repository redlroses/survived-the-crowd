using System;
using Sources.Pool;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    [RequireComponent(typeof(ProjectilePool))]
    public sealed class ProjectileShotMaker : MonoBehaviour, IShotMaker
    {
        [SerializeField] private ProjectilePool _pool;
        [SerializeField] private Transform _shotPoint;

        public event Action Shooting;

        private void Awake()
        {
            _pool ??= GetComponent<ProjectilePool>();
        }

        public void Construct(WeaponStaticData weaponData)
        {
            _pool.Construct(weaponData.Damage);
        }

        public void MakeShot()
        {
            _pool.Enable(_shotPoint.position, _shotPoint.rotation);
            Shooting?.Invoke();
        }
    }
}