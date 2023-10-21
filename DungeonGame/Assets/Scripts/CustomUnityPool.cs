using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class CustomUnityPool
    {
        private ObjectPool<GameObject> _pool;
        private GameObject _prefab;
        private Transform _parent;
        private Transform _parentOfParents;

        public CustomUnityPool(GameObject prefab, int prewarmObjectsCount, string parentName, Transform parentOfParents)
        {
            _prefab = prefab;
            _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy, false, maxSize: prewarmObjectsCount);
            _parentOfParents = parentOfParents.transform;
            _parent = new GameObject(parentName).transform;
            _parent.parent = _parentOfParents;
        }

        public GameObject Get()
        {
            var obj = _pool.Get();
            return obj;
        }

        public void Release(GameObject obj)
        {
            _pool.Release(obj);
        }

        private void OnDestroy(GameObject obj)
        {
            GameObject.Destroy(obj);
        }

        private void OnRelease(GameObject obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnGet(GameObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        private GameObject OnCreate()
        {
            var obj = GameObject.Instantiate(_prefab, _parent);
            return obj;
        }
    }
}