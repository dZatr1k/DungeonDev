using ObjectPool;
using System.Collections;
using UnityEngine;
using LevelLogic;

public class EnergySpawner : MonoBehaviour
{
    [SerializeField] private PoolsCatalog _poolsCatalog;
    [SerializeField] private Energy _energy;
    [SerializeField] private Transform _firstBorderPoint, _secondBorderPoint;
    [SerializeField] private float _minSpawnTime, _maxSpawnTime;
    
    private CustomUnityPool _pool;

    private void Start()
    {
        _pool = _poolsCatalog.GetPool(_energy);
    }

    private void OnEnable()
    {
        Level.Instance.CurrentStateMachine.OnGameStarted += StartSpawn;
    }

    private void OnDisable()
    {
        Level.Instance.CurrentStateMachine.OnGameStarted -= StartSpawn;
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnergyCoroutine());
    }

    private IEnumerator SpawnEnergyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
            var x = Random.Range(_firstBorderPoint.position.x, _secondBorderPoint.position.x);
            var y = Random.Range(_firstBorderPoint.position.y, _secondBorderPoint.position.y);
            var obj = _pool.Get();
            obj.transform.position = new Vector3(x, y, 0);
        }
    }
}
