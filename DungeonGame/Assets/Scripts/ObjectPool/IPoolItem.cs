using System;
using UnityEngine;

namespace ObjectPool
{
    public interface IPoolItem
    {
        public Type ItemType { get => GetType(); }
        public GameObject GameObject { get; }

        public void SetDefaultSettings();
    }
}
