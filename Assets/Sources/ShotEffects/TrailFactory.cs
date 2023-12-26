using Sources.Turret;
using UnityEngine;

namespace Sources.ShotEffects
{
    [RequireComponent(typeof(TrailPool))]
    public sealed class TrailFactory : MonoBehaviour
    {
        [SerializeField] private TrailPool _pool;
        [SerializeField] private RayShotMaker _rayShotMaker;
        [SerializeField] private Transform _shotTransform;

        private void Awake()
        {
            _pool ??= GetComponent<TrailPool>();
        }

        private void OnEnable()
        {
            _rayShotMaker.RayDamageDealer.RayHited += Draw;
        }

        private void OnDisable()
        {
            _rayShotMaker.RayDamageDealer.RayHited -= Draw;
        }

        private void Draw(RaycastHit hit)
        {
            ShotTrailView trail = _pool.Enable(Vector3.zero);
            trail.Create(_shotTransform.position, hit.point);
        }
    }
}