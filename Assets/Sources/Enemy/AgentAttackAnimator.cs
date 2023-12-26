using System.Collections;
using UnityEngine;

namespace Sources.Enemy
{
    public class AgentAttackAnimator : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private AgentAttackRangeTracker _rangeTracker;

        private Coroutine _attacking;
        private bool _isAttacking;
        private WaitForSeconds _waitForAttack;

        private IEnemyAnimator Animator => (IEnemyAnimator)_animator;

        private void Awake()
        {
            _waitForAttack = new WaitForSeconds(_attackSpeed);
        }

        private void OnEnable()
        {
            _rangeTracker.RangeEntered += OnStartAttack;
            _rangeTracker.RangedExited += OnStopAttack;
        }

        private void OnDisable()
        {
            OnStopAttack();
            _rangeTracker.RangeEntered -= OnStartAttack;
            _rangeTracker.RangedExited -= OnStopAttack;
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