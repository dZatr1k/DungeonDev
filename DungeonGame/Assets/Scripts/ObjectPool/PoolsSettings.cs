using ObjectPool;
using System;
using UnityEngine;

[Serializable]
public class PoolsSettings
{
    [SerializeField] private int _size;
    [SerializeField] private GameObject _gameObject;
    private IPoolItem _poolItem;
    public int Size => _size;
	public IPoolItem PoolItem
    {
        get
        {
            if (!_gameObject.TryGetComponent<IPoolItem>(out _poolItem))
                Debug.LogError($"{_gameObject.name} must be able to convert to IPoolItem");   
            return _poolItem;
        }
    }

}
    