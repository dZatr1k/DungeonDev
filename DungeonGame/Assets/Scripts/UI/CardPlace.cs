using UnityEngine;

namespace Card
{
    public class CardPlace : MonoBehaviour
    {
        private Card _placed;

        public Card Placed => _placed;

        public bool IsOccupied => _placed != null;

        public void Place(Card card)
        {
            if(_placed != null)
            {
                Debug.LogError("You are trying place card on occupied place!");
                return;
            }
            if(card == null)
            {
                Debug.LogError("You are trying place null-card. Use Free instead if you want to free place.");
                return;
            }

            _placed = card;
        }

        public void Free()
        {
            if (_placed == null)
            {
                Debug.LogError("You are trying free place which already was free.");
                return;
            }

            _placed = null;
        }

        public bool IsMyCard(Card card)
        {
            return card == _placed;
        }
    }
}