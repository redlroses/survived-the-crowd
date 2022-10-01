using System;
using UnityEngine;

public sealed class Projectile : MonoBehaviour, IPoolable<Projectile>
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damageValue;
    [SerializeField] private float _lifeTime;

    public event Action<Projectile> Disabled;

    private void Start()
    {
        Invoke(nameof(Disable), _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (_moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(_damageValue);
        }

        Disable();
    }

    private void Disable()
    {
        Disabled?.Invoke(this);
    }
}