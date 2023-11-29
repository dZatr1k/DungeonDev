using UnityEngine;
using TMPro;

public class EnergyResourcesPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        EnergyResourcesSystem.OnResourcesChanged += UpdatePresenter;
    }

    private void OnDisable()
    {
        EnergyResourcesSystem.OnResourcesChanged -= UpdatePresenter;
    }

    private void UpdatePresenter(uint energyUnits)
    {
        _text.text = energyUnits.ToString();
    }
}
