using Sources.Editor;
using Sources.Health;
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
            BaseEnemy.Damaged += ApplyDamagedColor;
        }

        private void OnDisable()
        {
            if (_renderer.material == _baseMaterial)
            {
                BaseEnemy.Damaged -= ApplyDamagedColor;
            }
        }

        private void ApplyDamagedColor()
        {
            _renderer.material = _damagedMaterial;
            BaseEnemy.Damaged -= ApplyDamagedColor;
        }

        private void ApplyBaseColor()
        {
            _renderer.material = _baseMaterial;
        }
    }
}