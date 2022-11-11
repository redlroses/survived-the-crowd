using Sources.DamageDeal;
using UnityEngine;

namespace Sources.ShotEffects
{
    [RequireComponent(typeof(TrailPool))]
    public class TrailFactory : MonoBehaviour
    {
        [SerializeField] private Transform _shotTransform;
        [SerializeField] private TrailPool _pool;

        private void Awake()
        {
            _pool ??= GetComponent<TrailPool>();
        }

        private void OnEnable()
        {
            DamageDealer.RayHited += Draw;
        }

        private void OnDisable()
        {
            DamageDealer.RayHited -= Draw;
        }

        private void Draw(RaycastHit hit)
        {
            ShotTrailView trail = _pool.Enable(Vector3.zero);
            trail.Create(_shotTransform.position, hit.point);
        }
    }
}