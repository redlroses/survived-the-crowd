using Sources.Enemy;
using UnityEngine;

namespace Sources
{
    public class HitFxPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fx;
        [SerializeField] private EnemyHealth _health;

        private void OnEnable()
        {
            _health.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _health.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _fx.Play();
        }
    }
}