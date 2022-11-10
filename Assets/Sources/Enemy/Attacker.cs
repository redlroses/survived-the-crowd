using System.Linq;
using Sources.Custom;
using Sources.HealthLogic;
using UnityEngine;

namespace Sources.Enemy
{
    public class Attacker : MonoBehaviour
    {
        private readonly Collider[] _hits = new Collider[2];

        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;

        [Space][Header("Hit")]
        [SerializeField] private Transform _hitCenter;
        [SerializeField] private float _hitRadius;
        [SerializeField] private LayerMask _layer;

        [Space][Header("Attack")]
        [SerializeField] private int _hitDamage;

        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

        private void OnEnable()
        {
            Animator.AttackCarried += OnAttackCarried;
        }

        private void OnDisable()
        {
            Animator.AttackCarried -= OnAttackCarried;
        }

        private void OnAttackCarried()
        {
            Attack();
        }

        private void Attack()
        {
            if (TryHit(out IDamageable damageable))
            {
                damageable.Damage(_hitDamage);
            }
        }

        private bool TryHit(out IDamageable damageable)
        {
            damageable = null;
            int hitsCount = Physics.OverlapSphereNonAlloc(_hitCenter.position, _hitRadius, _hits, _layer);
            return hitsCount > 0 && _hits[0].gameObject.TryGetComponent(out damageable);
        }
    }
}