using System;
using UnityEngine;
using Units.Heroes;

public class EnergyResourcesSystem : MonoBehaviour
{
    [SerializeField] private uint _energyUnits;

    public static event Action<uint> OnResourcesChanged;

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
        OnResourcesChanged?.Invoke(_energyUnits);
    }

    private void IncreaseEnergyResources(Energy energy)
    {
        _energyUnits += energy.EnergyUnits;
        OnResourcesChanged?.Invoke(_energyUnits);
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
            OnResourcesChanged?.Invoke(_energyUnits);
            return;
        }

    }
}