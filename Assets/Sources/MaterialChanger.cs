using UnityEngine;

public sealed class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Material _damagedMaterial;
    [SerializeField] private BaseEnemy _baseEnemy;
    [SerializeField] private Renderer _renderer;

    private void OnEnable()
    {
        ApplyBaseColor();
        _baseEnemy.Damaged += ApplyDamagedColor;
    }

    private void OnDisable()
    {
        if (_renderer.material == _baseMaterial)
        {
            _baseEnemy.Damaged -= ApplyDamagedColor;
        }
    }

    private void ApplyDamagedColor(int damage)
    {
        _renderer.material = _damagedMaterial;
        _baseEnemy.Damaged -= ApplyDamagedColor;
    }

    private void ApplyBaseColor()
    {
        _renderer.material = _baseMaterial;
    }
}