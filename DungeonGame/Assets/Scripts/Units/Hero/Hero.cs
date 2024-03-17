using UnityEngine;
using GameBoard;

namespace Units.Heroes
{
    public abstract class Hero : Unit
    {       
        public void SetObserveArea()
        {
            if (Weapon == null)
                return;

            Field field = transform.parent.parent.GetComponent<DataForUnits>().Field;
            Vector2 delta = field.EndPoint.position - transform.position;
            _observeArea.size = new Vector2(delta.x, _observeArea.size.y);
            _observeArea.offset = new Vector2(delta.x / 2, _observeArea.offset.y);
        }

    }
}
