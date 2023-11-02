using ObjectPool;
using UnityEngine;

public class PoolItemReleaser : MonoBehaviour
{
    [SerializeField] private PoolsCatalog _poolCatalog;

    private void OnEnable()
    {
        PoolItem.PoolItemReleased += ReleasePoolItem;
    }

    private void OnDisable()
    {
        PoolItem.PoolItemReleased -= ReleasePoolItem;
    }

    private void ReleasePoolItem(PoolItem poolItem)
    {
        var pool = _poolCatalog.GetPool(poolItem);
        pool.Release(poolItem);
    }
}
