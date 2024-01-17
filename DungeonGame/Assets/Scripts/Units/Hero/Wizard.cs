using System.Collections;
using UnityEngine;
using ObjectPool;

namespace Units.Heroes
{
    public class Wizard : Hero
    {
        [Space]
        [SerializeField] private Energy _energyPrefab;
        [SerializeField] private Transform _spawnPoint;

        private CustomUnityPool _energyPool;
        private Energy _currentEnergy;

        private void Start()
        {
            _energyPool = PoolsCatalog.Instance.GetPool(_energyPrefab);
            StartCoroutine(SpawnEnergyCoroutine());
        }

        private IEnumerator SpawnEnergyCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(RandomExtensions.GetNumberInEpsilonAmbit(_abilityCooldown));
                _currentEnergy = _energyPool.Get() as Energy;
                _currentEnergy.transform.position = _spawnPoint.position;

                _currentEnergy.OnEnergyDisable += UnlockSpawn;

                yield return new WaitUntil(() => _currentEnergy == null);
            }
        }

        private void UnlockSpawn()
        {
            _currentEnergy.OnEnergyDisable -= UnlockSpawn;
            _currentEnergy = null;
        }
    }
}
