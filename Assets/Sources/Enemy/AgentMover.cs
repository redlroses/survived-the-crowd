using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public sealed class AgentMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _target;

        public Transform Target => _target;

        public void ApplyTarget(Transform target)
        {
            _target = target;
        }
    }
}