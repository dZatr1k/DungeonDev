using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemies;

namespace Units.Heroes
{
    public abstract class Hero : Unit<Enemy>
    {
        [SerializeField] private float _reloadAfterBuyTime;

        public float ReloadAfterBuyTime => _reloadAfterBuyTime;
    }
}
