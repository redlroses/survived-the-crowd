using UnityEngine;

namespace Sources.Enemy
{
    public class AgentIdleArea : MonoBehaviour
    {
        [SerializeField] private Collider _idleCollider;

        public Vector3 GetRandomPosition()
        {
            Bounds bounds = _idleCollider.bounds;
            float randomXPosition = Random.Range(bounds.min.x, bounds.max.x);
            float randomZPosition = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 randomInnerPosition = new Vector3(randomXPosition, _idleCollider.bounds.center.y, randomZPosition);

            return _idleCollider.ClosestPoint(randomInnerPosition);
        }
    }
}