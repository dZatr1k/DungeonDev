using System;
using UnityEngine;

namespace ObjectPool
{
    public abstract class PoolItem : MonoBehaviour
    {
        public virtual Type ItemType { get; }
        public virtual GameObject GameObject { get; }

        public static event Action<PoolItem> PoolItemReleased;

        public virtual void SetDefaultSettings() { }

        protected virtual void ReleaseItem()
        {
            PoolItemReleased?.Invoke(this);
        }
    }

}
