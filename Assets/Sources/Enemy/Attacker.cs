using System.Linq;
using Sources.HealthLogic;
using UnityEngine;

namespace Sources.Enemy
{
    public class Attacker : MonoBehaviour
    {
        [Header("Hit")]
        [SerializeField] private Transform _hitCenter;
        [SerializeField] private float _hitRadius;
        [SerializeField] private LayerMask _layer;
        [Space] [Header("Attack")]
        [SerializeField] private int _attackDamage;

        private Collider[] _hits = new Collider[1];

        public void OnAttack()
        {
            Attack();
        }

        private void Attack()
        {
            if (TryHit(out IDamageable damageable))
            {
                damageable.Damage(_attackDamage);
            }
        }

        private bool TryHit(out IDamageable damageable)
        {
            damageable = null;
            int hitsCount = Physics.OverlapSphereNonAlloc(_hitCenter.position, _hitRadius, _hits, _layer);
            return hitsCount > 0 && _hits.FirstOrDefault().TryGetComponent(out damageable);
        }
    }
}