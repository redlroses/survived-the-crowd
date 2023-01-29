using System;
using Sources.Audio;
using Sources.DamageDeal.Data;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class RayShotMaker : MonoBehaviour, IShotMaker, IAudioPlayable
    {
        [SerializeField] private int _shotDamage;
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private LayerMask _layer;

        private RayDamageDealer _rayDamageDealer = new RayDamageDealer();

        public event Action AudioPlayed;
        public event Action ShotOff;

        public RayDamageDealer RayDamageDealer => _rayDamageDealer;

        public void Construct(WeaponStaticData weaponData)
        {
            _shotDamage = weaponData.Damage;
        }

        public void MakeShot()
        {
            Ray ray = new Ray(_shotPoint.position, _shotPoint.forward);
            RayCastData data = new RayCastData(ray, _layer);
            ShotOff?.Invoke();
            AudioPlayed?.Invoke();
            _rayDamageDealer.DealDamage(_shotDamage, data);
        }
    }
}
