using Sources.HealthLogic;
using UnityEngine;

namespace Sources
{
    public sealed class MaterialChanger : MonoBehaviour
    {
        [SerializeField] private Material _baseMaterial;
        [SerializeField] private Material _damagedMaterial;

        [RequireInterface(typeof(IDamageable))]
        [SerializeField] private MonoBehaviour _damagable;
        [SerializeField] private Renderer _renderer;

        private IDamageable BaseEnemy => (IDamageable) _damagable;

        private void OnEnable()
        {
            ApplyBaseColor();
            BaseEnemy.Changed += ApplyDamagedColor;
        }

        private void OnDisable()
        {
            if (_renderer.material == _baseMaterial)
            {
                BaseEnemy.Changed -= ApplyDamagedColor;
            }
        }

        private void ApplyDamagedColor()
        {
            _renderer.material = _damagedMaterial;
            BaseEnemy.Changed -= ApplyDamagedColor;
        }

        private void ApplyBaseColor()
        {
            _renderer.material = _baseMaterial;
        }
    }
}