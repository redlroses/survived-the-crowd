using Sources.Custom;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public sealed class AgentToTargetMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _attackable;

        private IAttackable Attackable => (IAttackable) _attackable;

        private void Update()
        {
            MoveToAttacable();
        }

        private void MoveToAttacable()
        {
            var attackPoint = Attackable.GetAttackPoint(transform.position);

            if (ReachedTarget(attackPoint) == false)
            {
                _agent.destination = attackPoint;
            }
        }

        private bool ReachedTarget(Vector3 attackPoint)
            => Vector3.Distance(attackPoint, transform.position) <= _agent.stoppingDistance;

        public void ApplyTarget(IAttackable target)
        {
            _attackable = (MonoBehaviour) target;
        }
    }

    public interface IAttackable
    {
        public Vector3 GetAttackPoint(Vector3 attackerPosition);
    }
}