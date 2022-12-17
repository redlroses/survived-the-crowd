using Sources.Tools;
using UnityEngine;

namespace Sources.Collectables
{
    public class CarDetailsSpawnZone : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public Vector3 GetRandomPosition()
        {
            float randomRadius = Random.Range(0, _radius);
            float randomAngle = Random.Range(0, (float) Constants.TwoPiDegrees);

            float randomXPosition = randomRadius * Mathf.Cos(randomAngle);
            float randomZPosition = randomRadius * Mathf.Sin(randomAngle);

            Vector3 worldPosition = transform.position;
            return new Vector3(randomXPosition + worldPosition.x, worldPosition.y, randomZPosition + worldPosition.z);
        }
    }
}