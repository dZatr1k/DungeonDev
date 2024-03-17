using Units.Heroes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardSettings", menuName = "Scriptable Object/CardSettings")]
public class CardSettings : ScriptableObject
{
    [SerializeField] private uint _cost;
    [SerializeField] private Hero _hero;
    [SerializeField] private float _reloadingTimeAfterBuy;

    public uint Cost => _cost;
    public Hero Hero => _hero;
    public float ReloadingTimeAfterBuy => _reloadingTimeAfterBuy;
}
