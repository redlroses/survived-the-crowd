using System;
using UnityEngine;

namespace Sources.Enemy
{
    public sealed class AgentPatternSwitcher : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _player;
        [SerializeField] private AgentToTargetMover _toTargetMover;
        [SerializeField] private AgentIdleMover _idleMover;

        [Space] [Header("Settings")]
        [SerializeField] private float _aggressionRadius;

        private bool _isAggressive = false;

        private IAttackable Player => (IAttackable) _player;

        private void OnEnable()
        {
            SwitchToIdlePattern();
            _isAggressive = false;
        }

        private void Update()
        {
            if (_isAggressive == false && IsPlayerInAggressiveRadius() && Player.IsAttackable)
            {
                SwitchToAggressivePattern();
                _isAggressive = true;
            }
            else if (_isAggressive && Player.IsAttackable == false)
            {
                SwitchToIdlePattern();
                _isAggressive = false;
            }
        }

        public void Init(IAttackable player)
        {
            _player = (MonoBehaviour) player ?? throw new ArgumentNullException();
        }

        private void SwitchToAggressivePattern()
        {
            _idleMover.enabled = false;
            _toTargetMover.enabled = true;
        }

        private void SwitchToIdlePattern()
        {
            _idleMover.enabled = true;
            _toTargetMover.enabled = false;
        }

        private bool IsPlayerInAggressiveRadius()
        {
            Vector3 position = transform.position;
            return Vector3.Distance(position, Player.GetAttackPoint(position)) <= _aggressionRadius;
        }
    }
}
