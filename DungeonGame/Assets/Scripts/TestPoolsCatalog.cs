using System;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class TestPoolsCatalog : MonoBehaviour
{
    [SerializeField] private PoolsCatalog _poolsCatalog;
    private CustomUnityPool _pool;

    private Dictionary<Type, List<GameObject>> _objectsFormPool = new Dictionary<Type, List<GameObject>>();

    private void GetItem<T>(T obj) where T : MonoBehaviour
    {
        var type = obj.GetType();
        _pool = _poolsCatalog.GetPool(obj);
        if (!_objectsFormPool.ContainsKey(type))
        {
            var listForDict = new List<GameObject>();
            _objectsFormPool.Add(obj.GetType(), listForDict);
        }
        _objectsFormPool[type].Add(_pool.Get());
    }


    public void GetItem(Energy obj)
    {
        GetItem<Energy>(obj);
    }

    public void GetItem(Skull obj)
    {
        GetItem<Skull>(obj);
    }

    public void RelaseItem(Energy obj)
    {
        RelaseItem<Energy>(obj);
    }

    public void RelaseItem(Skull obj)
    {
        RelaseItem<Skull>(obj);
    }


    private void RelaseItem<T>(T obj) where T : MonoBehaviour
    {
        var type = obj.GetType();
        _pool = _poolsCatalog.GetPool(obj);
        if (!_objectsFormPool.ContainsKey(type))
        {
            Debug.LogError("Uncorrect Type of Object");
        }
        var listOfObjects = _objectsFormPool[type];
        var objectForRelase = listOfObjects[listOfObjects.Count - 1];

        listOfObjects.Remove(objectForRelase);
        _pool.Release(objectForRelase);
    }
}
