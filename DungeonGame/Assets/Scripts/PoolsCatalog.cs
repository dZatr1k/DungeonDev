using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class PoolsCatalog : MonoBehaviour
    {
        private readonly Dictionary<Type, CustomUnityPool> _pools = new Dictionary<Type, CustomUnityPool>();
        private GameObject _parent;

        private void Start()
        {
            _parent = new GameObject("Pools");
        }

        public CustomUnityPool GetPool<T>(T obj) where T : MonoBehaviour
        {
            Type typeOfObject = obj.GetType();

            if (!_pools.ContainsKey(typeOfObject))
            {
                _pools.Add(typeOfObject, new CustomUnityPool(obj.gameObject, 7, typeOfObject.ToString(), _parent.transform));
            }
            return _pools[typeOfObject];
        }
    }

}
