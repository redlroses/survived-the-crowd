using System;
using UnityEngine;

public class Creature : MonoBehaviour, IDamageable
{
    [SerializeField, Min(1)] private int _maxHealth;

    public event Action<int> Damaged;
    
    public void Damage(int value)
    {
        if (IsAlive == false)
        {
            throw new InvalidOperationException();
        }
        
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Health -= value;

        if (Health <= 0)
        {
            Health = 0;
            IsAlive = false;
        }
        
        Damaged?.Invoke(Health);
    }

    public bool IsAlive { get; private set; }

    public int Health { get; private set; }
}
