using UnityEngine;

namespace Sources
{
    public interface IAttackable
    {
        public bool IsAttackable { get; }

        public Vector3 GetAttackPoint(Vector3 attackerPosition);
    }
}