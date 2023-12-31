using UnityEngine;
using GameBoard;

namespace Units.Heroes
{
    public abstract class Hero : Unit
    {
        [SerializeField] private float _reloadAfterBuyTime;
        
        public float ReloadAfterBuyTime => _reloadAfterBuyTime;
        
        public void ChangeObserveArea()
        {
            if (_weapon == null)
                return;

            Field field = transform.parent.parent.GetComponent<DataForUnits>().Field;
            Vector2 delta = field.EndPoint.position - transform.position;
            _observeArea.size = new Vector2(delta.x, _observeArea.size.y);
            _observeArea.offset = new Vector2(delta.x / 2, _observeArea.offset.y);
        }

    }
}
