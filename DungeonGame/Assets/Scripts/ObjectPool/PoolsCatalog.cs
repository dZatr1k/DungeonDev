using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class PoolsCatalog : MonoBehaviour
    {
        private static PoolsCatalog _instance;
        public static PoolsCatalog Instance => _instance;

        [SerializeField] private List<PoolsSettings> _poolsSettingsPair;
        [SerializeField] private int _defaultPoolSize;
        [SerializeField] private Transform _parent;

        private readonly Dictionary<Type, CustomUnityPool> _pools = new Dictionary<Type, CustomUnityPool>();

        private void OnValidate()
        {
            if (_instance != null)
            {
                Debug.LogError("Component PoolsCatalog already exist in current scene");
                return;
            }
            _instance = this;
        }

        private void Awake()
        {
            // сделал тут так как в методе Start EnergySpawner уже PoolsCatalog должен быть настроен
            for (int i = 0; i < _poolsSettingsPair.Count; i++)
            {
                var poolItem = _poolsSettingsPair[i].PoolItem;
                var pool = new CustomUnityPool(poolItem, _poolsSettingsPair[i].Size, poolItem.ItemType.ToString(), _parent);
                _pools.Add(poolItem.ItemType, pool);
            }
        }

        public CustomUnityPool GetPool(PoolItem obj)
        {
            Type typeOfObject = obj.ItemType;
            if (!_pools.ContainsKey(typeOfObject))
            {
                _pools.Add(typeOfObject, new CustomUnityPool(obj, _defaultPoolSize, typeOfObject.ToString(), _parent));
            }
            return _pools[typeOfObject];
        }
    }

}
