using Sources.Custom;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public class AgentMoveAnimator : MonoBehaviour
    {
        private const float MinimalVelocity = 0.001f;

        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;
        [SerializeField] private NavMeshAgent _agent;

        private bool _isMoving;

        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

        private void Update()
        {
            if (IsAgentMoving())
            {
                Animator.SetSpeed(_agent.velocity.magnitude);

                if (_isMoving == false)
                {
                    StartMoveAnimation();
                }
            }
            else if (_isMoving)
            {
                FinishMoveAnimation();
            }
        }

        private void FinishMoveAnimation()
        {
            Animator.FinishMove();
            _isMoving = false;
        }

        private void StartMoveAnimation()
        {
            Animator.StartMove();
            _isMoving = true;
        }

        private bool IsAgentMoving()
            => _agent.velocity.magnitude > MinimalVelocity;
    }
}
