using System;
using UnityEngine;

namespace Sources.Enemy
{
    public sealed class AgentPatternSwitcher : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _player;
        [SerializeField] private AgentAttackRangeTracker _rangeTracker;
        [SerializeField] private AgentToTargetMover _toTargetMover;
        [SerializeField] private AgentIdleMover _idleMover;

        [Space] [Header("Settings")]
        [SerializeField] private float _aggressionRadius;

        private void OnEnable()
        {
            SwitchToIdlePattern();
        }

        private void Update()
        {
            if (IsPlayerInAggressiveRadius())
            {
                SwitchToAggressivePattern();
            }
        }

        public void Init(Transform player)
        {
            _player = player ? player : throw new NullReferenceException();
        }

        private void SwitchToAggressivePattern()
        {
            _rangeTracker.enabled = true;
            _idleMover.enabled = false;
            _toTargetMover.enabled = true;
        }

        private void SwitchToIdlePattern()
        {
            _rangeTracker.enabled = false;
            _idleMover.enabled = true;
            _toTargetMover.enabled = false;
        }

        private bool IsPlayerInAggressiveRadius()
            => Vector3.Distance(transform.position, _player.position) <= _aggressionRadius;
    }
}
