using Sources.DamageDeal;
using Sources.DamageDeal.Data;
using UnityEngine;

namespace Sources.Enemy
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;

        [Space][Header("Hit")]
        [SerializeField] private Transform _hitCenter;
        [SerializeField] private float _hitRadius;
        [SerializeField] private LayerMask _layer;

        [Space][Header("Attack")]
        [SerializeField] private int _hitDamage;

        private OverlapSphereDamageDealer _damageDealer;

        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

        private void Awake()
        {
            _damageDealer = new OverlapSphereDamageDealer();
        }

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
            OverlapSphereData data = new OverlapSphereData(_hitCenter.position, _hitRadius, _layer);
            _damageDealer.DealDamage(_hitDamage, data);
        }
    }
}