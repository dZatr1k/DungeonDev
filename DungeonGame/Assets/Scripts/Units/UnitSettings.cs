using UnityEngine;


namespace Units
{
    [CreateAssetMenu(fileName = "NewUnitSettings", menuName = "Scriptable Object/UnitSettings")]
    public class UnitSettings : ScriptableObject
    {
        [SerializeField] private uint _health;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _abilityCooldown;

        public uint Health => _health;
        public Weapon Weapon => _weapon;
        public float AbilityCooldown => _abilityCooldown;
    }
}
