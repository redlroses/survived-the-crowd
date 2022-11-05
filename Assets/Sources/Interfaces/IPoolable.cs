using System;
using UnityEngine;

public interface IPoolable<out T>
{
    event Action<T> Disabled;
}