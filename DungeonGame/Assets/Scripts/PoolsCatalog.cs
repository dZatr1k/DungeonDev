using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolsCatalog : MonoBehaviour 
{
    private readonly Dictionary<Type, CustomUnityPool> _pools = new Dictionary<Type, CustomUnityPool>();
   
    public CustomUnityPool GetPool<T>(T obj) where T : MonoBehaviour
    {
        Type typeOfObject = obj.GetType();

        if (!_pools.ContainsKey(typeOfObject))
        {
            _pools.Add(typeOfObject, new CustomUnityPool(obj.gameObject, 2));
        }
        return _pools[typeOfObject];
    }
}
