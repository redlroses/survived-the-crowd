using System;
using Sources.Audio;
using Sources.DamageDeal.Data;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class RayShotMaker : MonoBehaviour, IShotMaker, IAudioPlayable
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private int _shotDamage;
        [SerializeField] private Transform _shotPoint;

        public event Action AudioPlaying;

        public event Action Shooting;

        public RayDamageDealer RayDamageDealer { get; } = new RayDamageDealer();

        public void Construct(WeaponStaticData weaponData)
        {
            _shotDamage = weaponData.Damage;
        }

        public void MakeShot()
        {
            Ray ray = new Ray(_shotPoint.position, _shotPoint.forward);
            RayCastData data = new RayCastData(ray, _layer);
            Shooting?.Invoke();
            AudioPlaying?.Invoke();
            RayDamageDealer.DealDamage(_shotDamage, data);
        }
    }
}