using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemies;
using GameBoard;
using ObjectPool;
using LevelLogic;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Cell[] _cells;
    [SerializeField] private PoolsCatalog _catalog;
    [SerializeField] private float _spawnTime;

    private void OnEnable()
    {
        Level.Instance.CurrentStateMachine.OnGameStarted += StartSpawn;
    }

    private void OnDisable()
    {
        Level.Instance.CurrentStateMachine.OnGameStarted -= StartSpawn;
    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomExtensions.GetNumberInEpsilonAmbit(_spawnTime));
            int randomEnemy = Random.Range(0, _enemies.Length);
            var enemy = _catalog.GetPool(_enemies[randomEnemy]).Get();
            int randomCell = Random.Range(0, _cells.Length);
            enemy.transform.position = _cells[randomCell].transform.position;
        }
    }
}
