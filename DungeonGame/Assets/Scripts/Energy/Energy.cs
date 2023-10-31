using ObjectPool;
using UnityEngine;

public class Energy : MonoBehaviour, IPoolItem
{
    public GameObject GameObject => gameObject;

    public void SetDefaultSettings()
    {
        Debug.Log("Default Settings");
    }
}
