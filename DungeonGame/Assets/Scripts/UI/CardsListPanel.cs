using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardsListPanel : CardsPanel
    {
        protected new void OnValidate()
        {
            base.OnValidate();


            for (int i = 0; i < _places.Length; i++)
            {
                var card = _places[i].GetComponentInChildren<Card>();
                card.ID = i;
                if (!_places[i].IsOccupied)
                    _places[i].Place(card);

            }
        }

        public Vector3 GetPositionBy(int id)
        {
            return GetPlaceBy(id).GetComponent<RectTransform>().position;
        }

        public CardPlace GetPlaceBy(int id)
        {
            return _places[id];
        }

    }
}
