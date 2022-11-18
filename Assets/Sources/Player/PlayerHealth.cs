using Sources.HealthLogic;
using UnityEngine;

namespace Sources.Player
{
    public class PlayerHealth : Health, IAttackable
    {
        [SerializeField] private Collider _hurtBox;

        public Vector3 GetAttackPoint(Vector3 attackerPosition)
            => _hurtBox.ClosestPoint(attackerPosition);

        public Vector3 GetAttackCenter(Vector3 attackerPosition)
            => _hurtBox.transform.position;
    }
}