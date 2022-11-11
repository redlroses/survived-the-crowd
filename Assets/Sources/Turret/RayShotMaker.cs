using System;
using Sources.DamageDeal;
using Sources.DamageDeal.Data;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class RayShotMaker : MonoBehaviour, IShotMaker
    {
        [SerializeField] private int _shotDamage;
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private LayerMask _layer;

        public event Action ShotOff;

        public void MakeShot()
        {
            Ray ray = new Ray(_shotPoint.position, _shotPoint.forward);
            RayCastData data = new RayCastData(ray, _layer);
            DamageDealer.DealDamage(_shotDamage, data);
            ShotOff?.Invoke();
        }
    }
}
