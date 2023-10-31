using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class CustomUnityPool
    {
        private ObjectPool<IPoolItem> _pool;
        private IPoolItem _poolItem;
        private Transform _parent;
        private Transform _parentOfParents;

        public CustomUnityPool(IPoolItem prefab, int maxSize, string parentName, Transform parentOfParents)
        {
            _poolItem = prefab;
            _pool = new ObjectPool<IPoolItem>(OnCreate, OnGet, OnRelease, OnDestroy, false, maxSize: maxSize);
            _parentOfParents = parentOfParents.transform;
            _parent = new GameObject(parentName).transform;
            _parent.parent = _parentOfParents;
        }

        public IPoolItem Get()
        {
            var obj = _pool.Get();
            return obj;
        }

        public void Release(IPoolItem obj)
        {
            _pool.Release(obj);
        }

        private void OnDestroy(IPoolItem obj)
        {
            Object.Destroy(obj.GameObject);
        }

        private void OnRelease(IPoolItem obj)
        {
            obj.SetDefaultSettings();
            obj.GameObject.SetActive(false);
        }

        private void OnGet(IPoolItem obj)
        {
            obj.GameObject.SetActive(true);
        }

        private IPoolItem OnCreate()
        {
            var obj = Object.Instantiate(_poolItem.GameObject, _parent);
            return obj.GetComponent<IPoolItem>();
        }
    }
}