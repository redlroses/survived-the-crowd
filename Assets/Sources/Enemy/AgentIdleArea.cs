using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Enemy
{
    public class AgentIdleArea : MonoBehaviour
    {
        [SerializeField] private Collider _idleCollider;

        private Bounds _bounds;

        private void OnEnable()
        {
            _bounds = _idleCollider.bounds;
        }

        public Vector3 GetRandomPosition()
        {
            float randomXPosition = Random.Range(_idleCollider.bounds.min.x, _idleCollider.bounds.max.x);
            float randomZPosition = Random.Range(_idleCollider.bounds.min.z, _idleCollider.bounds.max.z);
            Vector3 randomInnerPosition = new Vector3(randomXPosition, _idleCollider.bounds.center.y, randomZPosition);
            return _idleCollider.ClosestPoint(randomInnerPosition);
        }
    }
}