using ObjectPool;
using System;
using UnityEngine;

[Serializable]
public class PoolsSettings
{
    [SerializeField] private int _size;
    [SerializeField] private PoolItem _poolItem;
    public int Size => _size;
    public PoolItem PoolItem => _poolItem;

}
    