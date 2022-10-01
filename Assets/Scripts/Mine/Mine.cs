using System;
using System.Collections;
using UnityEngine;

namespace Mine
{
    public sealed class Mine : MonoBehaviour
    {
        [SerializeField] private int _explosionDamage;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _detonationDelay;

        public event Action FuseActivated;
        public event Action Explosion;

        public float ExplosionRadius => _explosionRadius;
        public float ExplosionDamage => _explosionDamage;
        public float DetonationDelay => _detonationDelay;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                StartCoroutine(ActivateFuse());
            }
        }

#if UNITY_EDITOR
        [ContextMenu("ActivateManually")]
        public void ActivateManually()
        {
            StartCoroutine(ActivateFuse());
        }
#endif

        private IEnumerator ActivateFuse()
        {
            FuseActivated?.Invoke();

            yield return new WaitForSeconds(_detonationDelay);

            BlowUp();
        }

        private void BlowUp()
        {
            var colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(_explosionDamage);
                }
            }

            enabled = false;
            Explosion?.Invoke();
        }
    }
}