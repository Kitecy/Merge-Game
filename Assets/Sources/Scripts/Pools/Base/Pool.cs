using UnityEngine;
using UnityEngine.Pool;

namespace MergeGame
{
    public abstract class Pool<T> : MonoBehaviour where T : class, IPoolable<T>
    {
        private const int MinMaxPoolSize = 10;

        [SerializeField] protected T Prefab;

        protected ObjectPool<T> Instance = null;

        [SerializeField] private int _defaultCapacity;
        [SerializeField, Min(MinMaxPoolSize)] private int _maxSize;

        private void Awake()
        {
            Instance = new ObjectPool<T>(CreateObject, OnGetObject, OnReleaseObject, defaultCapacity: _defaultCapacity, maxSize: _maxSize);
        }

        public void Release(T poolableObject)
        {
            Instance.Release(poolableObject);
        }

        public T Get()
        {
            return Instance.Get();
        }

        protected abstract T CreateObject();

        protected abstract void OnGetObject(T poolableObject);

        protected abstract void OnReleaseObject(T poolableObject);
    }
}
