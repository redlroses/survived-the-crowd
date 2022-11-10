using Sources.HealthLogic;
using UnityEngine;

namespace Sources.Enemy
{
    public class EnemyHealth : Health, IAttackable
    {
        public Vector3 GetAttackPoint(Vector3 attackerPosition)
            => transform.position;

        public Vector3 GetAttackCenter(Vector3 attackerPosition)
            => transform.position;
    }
}