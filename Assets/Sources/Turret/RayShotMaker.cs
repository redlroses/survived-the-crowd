using System;
using Sources.DamageDeal.Data;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class RayShotMaker : MonoBehaviour, IShotMaker
    {
        [SerializeField] private int _shotDamage;
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private LayerMask _layer;

        private RayDamageDealer _rayDamageDealer = new RayDamageDealer();

        public event Action ShotOff;

        public RayDamageDealer RayDamageDealer => _rayDamageDealer;

        public void MakeShot()
        {
            Ray ray = new Ray(_shotPoint.position, _shotPoint.forward);
            RayCastData data = new RayCastData(ray, _layer);
            ShotOff?.Invoke();
            RayDamageDealer.DealDamage(_shotDamage, data);
        }
    }
}
