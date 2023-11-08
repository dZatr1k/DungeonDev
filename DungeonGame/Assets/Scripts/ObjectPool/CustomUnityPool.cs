using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class CustomUnityPool
    {
        private ObjectPool<PoolItem> _pool;
        private PoolItem _poolItem;
        private Transform _parent;
        private Transform _parentOfParents;

        public CustomUnityPool(PoolItem prefab, int maxSize, string parentName, Transform parentOfParents)
        {
            _poolItem = prefab;
            _pool = new ObjectPool<PoolItem>(OnCreate, OnGet, OnRelease, OnDestroy, false, maxSize: maxSize);
            _parentOfParents = parentOfParents.transform;
            _parent = new GameObject(parentName).transform;
            _parent.parent = _parentOfParents;
        }

        public PoolItem Get()
        {
            var obj = _pool.Get();
            return obj;
        }

        public void Release(PoolItem obj)
        {
            _pool.Release(obj);
        }

        private void OnDestroy(PoolItem obj)
        {
            Object.Destroy(obj.GameObject);
        }

        private void OnRelease(PoolItem obj)
        {
            obj.SetDefaultSettings();
            obj.GameObject.SetActive(false);
        }

        private void OnGet(PoolItem obj)
        {
            obj.GameObject.SetActive(true);
        }

        private PoolItem OnCreate()
        {
            return Object.Instantiate(_poolItem, _parent);
        }
    }
}