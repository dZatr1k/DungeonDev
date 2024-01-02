using GameBoard;
using ObjectPool;
using UnityEngine;

public class DataForUnits : MonoBehaviour
{
    [SerializeField] private Field _field;
    [SerializeField] private PoolsCatalog _poolsCatalog;

    public Field Field => _field;
    public PoolsCatalog PoolsCatalog => _poolsCatalog;
}
