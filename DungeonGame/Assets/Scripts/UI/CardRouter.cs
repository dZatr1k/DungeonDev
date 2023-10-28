using UnityEngine;
using DG.Tweening;

namespace Card
{
    public class CardRouter : MonoBehaviour
    {
        [SerializeField] private float _moveTime;
        [SerializeField] private MainCardsPanel _mainPanel;
        [SerializeField] private CardsListPanel _listPanel;

        private void OnEnable()
        {
            Card.OnClick += OnCardClick;
        }

        private void OnDisable()
        {
            Card.OnClick -= OnCardClick;
        }

        private void OnCardClick(Card card)
        {
            if (_mainPanel.IsPlacedHere(card))
            {
                var whereCardPlaced = _mainPanel.GetWherePlaced(card);
                var targetCardPlace = _listPanel.GetPlaceBy(card.ID);
                Vector3 targetPosition = _listPanel.GetPositionBy(card.ID);
                ChangeCardPlace(card, targetCardPlace, whereCardPlaced, targetPosition);

                CheckMainPanelGaps();
            }
            else
            {
                if (_mainPanel.IsAnyFree())
                {
                    var freeCardPlace = _mainPanel.GetFreeCardPlace();
                    var currentCardPlace = _listPanel.GetPlaceBy(card.ID);
                    Vector3 targetPosition = freeCardPlace.GetComponent<RectTransform>().position;
                    ChangeCardPlace(card, freeCardPlace, currentCardPlace, targetPosition);
                }
            }
        }

        private void ChangeCardPlace(Card card, CardPlace to, CardPlace from, Vector3 targetPosition)
        {
            to.Place(card);
            from.Free();

            card.transform.SetParent(to.transform);
            MoveCard(card, targetPosition);
        }

        private void CheckMainPanelGaps()
        {
            if (_mainPanel.HasGaps())
                FixMainPanelGaps();
        }

        private void FixMainPanelGaps()
        {
            do
            {
                var freeCardPlace = _mainPanel.GetFreeCardPlace();
                var gap = _mainPanel.GetGap();
                Vector3 targetPosition = freeCardPlace.GetComponent<RectTransform>().position;
                ChangeCardPlace(gap.Placed, freeCardPlace, gap, targetPosition);
            }
            while (_mainPanel.HasGaps());
        }

        private void MoveCard(Card card, Vector3 targetPosition)
        {
            card.GetComponent<RectTransform>().DOMove(targetPosition, _moveTime);
        }
    }
}