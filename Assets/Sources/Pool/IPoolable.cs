using System;

namespace Sources.Pool
{
    public interface IPoolable<out T>
    {
        event Action<T> Destroyed;
    }
}