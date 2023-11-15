using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace Units.Heroes
{
    public class Wizard : Hero
    {
        [SerializeField] private Energy _energyPrefab;
        [SerializeField] private Transform _spawnPoint;

        private PoolsCatalog _catalog;
        private void Start()
        {
            _catalog = FindObjectOfType<PoolsCatalog>();
            StartCoroutine(SpawnEnergyCoroutine());
        }

        private IEnumerator SpawnEnergyCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(RandomExtensions.GetNumberInEpsilonAmbit(_attackCooldown));
                _catalog.GetPool(_energyPrefab).Get().transform.position = _spawnPoint.position;
            }
        }
    }
}
