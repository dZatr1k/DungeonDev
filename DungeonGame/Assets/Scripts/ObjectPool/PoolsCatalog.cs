using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class PoolsCatalog : MonoBehaviour
    {
        [SerializeField] private List<PoolsSettings> _poolsSettingsPair;
        [SerializeField] private int _defaultPoolSize;

        private readonly Dictionary<Type, CustomUnityPool> _pools = new Dictionary<Type, CustomUnityPool>();
        private GameObject _parent;
        

        private void Start()
        {
            _parent = new GameObject("Pools");
            for (int i = 0; i < _poolsSettingsPair.Count; i++)
            {
                var pool = new CustomUnityPool(_poolsSettingsPair[i].Prefab, _poolsSettingsPair[i].Size,
                                               _poolsSettingsPair[i].ObjectType.ToString(), _parent.transform);
                _pools.Add(_poolsSettingsPair[i].ObjectType, pool);
            }

        }

        public CustomUnityPool GetPool<T>(T obj) where T : MonoBehaviour
        {
            Type typeOfObject = obj.GetType();

            if (!_pools.ContainsKey(typeOfObject))
            {
                _pools.Add(typeOfObject, new CustomUnityPool(obj.gameObject, _defaultPoolSize, typeOfObject.ToString(), _parent.transform));
            }
            return _pools[typeOfObject];
        }
    }

}
