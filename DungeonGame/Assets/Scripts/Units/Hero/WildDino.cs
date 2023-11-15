using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Heroes
{
    public class WildDino : Hero
    {
        private IEnumerator AttackCoroutine(Unit enemy)
        {
            yield return new WaitForSeconds(0.3f);
            base.Attack(enemy);
        }

        public override void Attack(Unit enemy)
        {
            base.Attack(enemy);
            StartCoroutine(AttackCoroutine(enemy));
        }
    }
}