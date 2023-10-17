using UnityEngine;
using UnityEngine.Pool;

public class CustomUnityPool
{
    private ObjectPool<GameObject> _pool;

    private GameObject _prefab;

    public CustomUnityPool(GameObject prefab, int prewarmObjectsCount)
    {
        _prefab = prefab;
        _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy, false, maxSize:prewarmObjectsCount);
    }

    public GameObject Get()
    {
        var obj = _pool.Get();
        return obj;
    }

    public void Release(GameObject obj)
    {
        _pool.Release(obj);
    }

    /// <summary>
    /// КАК уничтожаем объект
    /// </summary>
    /// <param name="obj"></param>
    private void OnDestroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }

    /// <summary>
    /// Что делаем с объектом когда его деактивируем
    /// </summary>
    /// <param name="obj"></param>
    private void OnRelease(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    /// <summary>
    /// Что делаем с объектом когда достаём его из пула
    /// </summary>
    /// <param name="obj"></param>
    private void OnGet(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    /// <summary>
    /// КАК мы создаём объект
    /// </summary>
    /// <returns></returns>
    private GameObject OnCreate()
    {
        return GameObject.Instantiate(_prefab);
    }
}