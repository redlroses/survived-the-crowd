using System.Collections;
using UnityEngine;

namespace Sources.Enemy
{
    public class AgentAttackAnimator : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;
        [SerializeField] private AgentAttackRangeTracker _rangeTracker;
        [SerializeField] private float _attackSpeed;

        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

        private Coroutine _attacking;
        private WaitForSeconds _waitForAttack;
        private bool _isAttacking;

        private void Awake()
        {
            _waitForAttack = new WaitForSeconds(_attackSpeed);
        }

        private void OnEnable()
        {
            _rangeTracker.EnteredRange += OnStartAttack;
            _rangeTracker.OutOfRange += OnStopAttack;
        }

        private void OnDisable()
        {
            OnStopAttack();
            _rangeTracker.EnteredRange -= OnStartAttack;
            _rangeTracker.OutOfRange -= OnStopAttack;
        }

        private IEnumerator Attacking()
        {
            while (_isAttacking)
            {
                Animator.PlayAttack();
                yield return _waitForAttack;
            }
        }

        private void OnStartAttack()
        {
            _isAttacking = true;
            _attacking ??= StartCoroutine(Attacking());
        }

        private void OnStopAttack()
        {
            _isAttacking = false;

            if (_attacking != null)
            {
                StopCoroutine(_attacking);
            }

            _attacking = null;
        }
    }
}