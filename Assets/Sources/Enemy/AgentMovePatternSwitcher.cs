using UnityEngine;

namespace Sources.Enemy
{
    public sealed class AgentMovePatternSwitcher : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _player;
        [SerializeField] private AgentToTargetMover _toTargetMover;
        [SerializeField] private AgentIdleMover _idleMover;

        [Space] [Header("Settings")]
        [SerializeField] private float _aggressionRadius;

        private void Update()
        {
            if (IsPlayerInAggressiveRadius())
            {
                SwitchToAggressiveMovePattern();
            }
        }

        private void SwitchToAggressiveMovePattern()
        {
            _idleMover.enabled = false;
            _toTargetMover.enabled = true;
        }

        private bool IsPlayerInAggressiveRadius()
            => Vector3.Distance(transform.position, _player.position) <= _aggressionRadius;
    }
}
