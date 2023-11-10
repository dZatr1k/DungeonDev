using System;
using UnityEngine;
using Units.Heroes;

public class EnergyResourcesSystem : MonoBehaviour
{
    [SerializeField] private uint _energyUnits;

    public static event Action<uint> ResourcesChanged;

    private void OnEnable()
    {
        Energy.EnergyCollected += IncreaseEnergyResources;
    }

    private void OnDisable()
    {
        Energy.EnergyCollected -= IncreaseEnergyResources;
    }

    private void Start()
    {
        ResourcesChanged?.Invoke(_energyUnits);
    }

    private void IncreaseEnergyResources(Energy energy)
    {
        _energyUnits += energy.EnergyUnits;
        ResourcesChanged?.Invoke(_energyUnits);
    }

    public bool IsAbleToBuy(Hero hero)
    {
        return hero.Cost <= _energyUnits;
    }

    public void DecreaseEnergyResources(Hero hero)
    {
        if (hero.Cost <= _energyUnits)
        {
            _energyUnits -= hero.Cost;
            ResourcesChanged?.Invoke(_energyUnits);
            return;
        }

    }
}