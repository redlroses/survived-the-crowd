using UnityEngine;

namespace Sources
{
    public static class RangeChecker
    {
        public static bool CanAttack(Vector3 attackerPosition, Collider target, float attackRange)
        {
            Vector3 closestTargetPoint = target.ClosestPoint(attackerPosition);
            float currentDistance = Vector3.Distance(attackerPosition, closestTargetPoint);
            return currentDistance <= attackRange;
        }

        public static bool CanAttack(Vector3 attackerPosition, Vector3 attackTarget, float attackRange)
        {
            float currentDistance = Vector3.Distance(attackerPosition, attackTarget);
            return currentDistance <= attackRange;
        }
    }
}