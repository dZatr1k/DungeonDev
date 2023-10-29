using ObjectPool;
using UnityEngine;

public class Energy : MonoBehaviour, IPoolItem
{
    public void SetDefaultSettings()
    {
        Debug.Log("Default Settings");
    }
}
