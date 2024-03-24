using UnityEngine;
using LevelLogic;
using DG.Tweening;

namespace Card
{
    public class CardsListPanel : CardsPanel
    {
        [SerializeField] private float _hideTime;
        [SerializeField] private float _hideYPosition;

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

        private void OnEnable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted += Hide;
        }

        private void OnDisable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted -= Hide;
        }

        public Vector3 GetPositionBy(int id)
        {
            return GetPlaceBy(id).GetComponent<RectTransform>().position;
        }

        public CardPlace GetPlaceBy(int id)
        {
            return _places[id];
        }

        public void Hide()
        {
            transform.DOMoveY(_hideYPosition, _hideTime);
        }

    }
}
