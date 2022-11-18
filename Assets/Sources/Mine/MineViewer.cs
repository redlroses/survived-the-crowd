using UnityEngine;

namespace Sources.Mine
{
    public sealed class MineViewer : MonoBehaviour
    {
        [SerializeField] private global::Sources.Mine.Mine _mine;
        [SerializeField] private GameObject _explosionEffect;
        [SerializeField] private GameObject _meshes;
        [SerializeField] private Collider _collider;

        private void OnEnable()
        {
            _mine.Explosion += OnExplosion;
            _mine.FuseActivated += OnFuseActivated;
        }

        private void OnDisable()
        {
            _mine.Explosion -= OnExplosion;
            _mine.FuseActivated -= OnFuseActivated;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _mine.ExplosionRadius);
            Gizmos.color = Color.white;
        }

        private void OnFuseActivated()
        {
            _mine.FuseActivated -= OnFuseActivated;
            _collider.enabled = false;
        }

        private void OnExplosion()
        {
            _collider.enabled = false;
            _mine.Explosion -= OnExplosion;
            _explosionEffect.SetActive(true);
            _meshes.SetActive(false);
        }
    }
}
