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
            float randomXPosition = Random.Range(_bounds.min.x, _bounds.max.x);
            float randomZPosition = Random.Range(_bounds.min.z, _bounds.max.z);
            Vector3 randomInnerPosition = new Vector3(randomXPosition, _bounds.center.y, randomZPosition);
            return _idleCollider.ClosestPoint(randomInnerPosition);
        }
    }
}