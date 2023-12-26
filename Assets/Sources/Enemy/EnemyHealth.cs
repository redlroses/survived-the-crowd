using Sources.HealthLogic;
using UnityEngine;

namespace Sources.Enemy
{
    public class EnemyHealth : Health, IAttackable
    {
        public bool IsAttackable => gameObject.activeInHierarchy;

        public Vector3 GetAttackPoint(Vector3 attackerPosition)
            => transform.position;
    }
}