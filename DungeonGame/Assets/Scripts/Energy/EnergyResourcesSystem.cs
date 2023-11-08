using System;
using UnityEngine;

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

    public bool TryDecreaseEnergyResources(uint cost) // after I suppose that here we'll pass as arg special class or interface, which contain cost property 
    {
        if (cost <= _energyUnits)
        {
            _energyUnits -= cost;
            ResourcesChanged?.Invoke(_energyUnits);
            return true;
        }
        return false;
    }
}