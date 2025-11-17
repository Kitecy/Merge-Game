using System;

namespace MergeGame
{
    public interface IPoolable<T> where T : class, IPoolable<T>
    {
        event Action Released;

        Pool<T> Pool { get; }

        void SetPool(Pool<T> pool);

        void Release();
    }
}
