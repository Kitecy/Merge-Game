using System;
using UnityEngine;

namespace MergeGame
{
    public class PoolableObject<T> : MonoBehaviour, IPoolable<T> where T : class, IPoolable<T>
    {
        public event Action Released;

        [field: SerializeField] public Pool<T> Pool { get; private set; }

        #region IPoolable
        public void SetPool(Pool<T> pool)
        {
            Pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public void Release()
        {
            Pool.Release((T)(object)this);

            OnReleased();

            Released?.Invoke();
        }
        #endregion

        protected virtual void OnReleased() { }
    }
}