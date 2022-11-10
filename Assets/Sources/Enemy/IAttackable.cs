using UnityEngine;

namespace Sources.Enemy
{
    public interface IAttackable
    {
        public Vector3 GetAttackPoint(Vector3 attackerPosition);
        public Vector3 GetAttackCenter(Vector3 attackerPosition);
    }
}