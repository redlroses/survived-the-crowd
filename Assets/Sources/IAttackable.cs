using UnityEngine;

namespace Sources
{
    public interface IAttackable
    {
        public Vector3 GetAttackPoint(Vector3 attackerPosition);
    }
}