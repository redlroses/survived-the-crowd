using System;
using Hole;
using UnityEngine;

public sealed class BaseEnemy : Creature, IPoolable<BaseEnemy>
{
    public event Action<BaseEnemy> Disabled;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        Damaged += OnDamaged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DeadZone _))
        {
            Disabled?.Invoke(this);
        }
    }

    private void OnDisable()
    {
        Damaged -= OnDamaged;
    }

    private void OnDamaged(int health)
    {
        if (health <= 0)
        {
            Disabled?.Invoke(this);
        }
    }
}
