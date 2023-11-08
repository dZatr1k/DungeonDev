using UnityEngine;
using DG.Tweening;

namespace Card
{
    public class CardSwapper : ICardBehaviour
    {
        private readonly MainCardsPanel _mainCardsPanel;
        private readonly CardsListPanel _cardListPanel;
        private readonly float _moveBetweenPanelsTime;

        public CardSwapper(MainCardsPanel mainCardsPanel, CardsListPanel cardListPanel, float moveBetweenPanelsTime)
        {
            _mainCardsPanel = mainCardsPanel;
            _cardListPanel = cardListPanel;
            _moveBetweenPanelsTime = moveBetweenPanelsTime;
        }

        public void OnClick(Card card)
        {
            if (_mainCardsPanel.IsPlacedHere(card))
            {
                var whereCardPlaced = _mainCardsPanel.GetWherePlaced(card);
                var targetCardPlace = _cardListPanel.GetPlaceBy(card.ID);
                Vector3 targetPosition = _cardListPanel.GetPositionBy(card.ID);
                ChangeCardPlace(card, targetCardPlace, whereCardPlaced, targetPosition);

                CheckMainPanelGaps();
            }
            else
            {
                if (_mainCardsPanel.IsAnyFree())
                {
                    var freeCardPlace = _mainCardsPanel.GetFreeCardPlace();
                    var currentCardPlace = _cardListPanel.GetPlaceBy(card.ID);
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
            if (_mainCardsPanel.HasGaps())
                FixMainPanelGaps();
        }

        private void FixMainPanelGaps()
        {
            do
            {
                var freeCardPlace = _mainCardsPanel.GetFreeCardPlace();
                var gap = _mainCardsPanel.GetGap();
                Vector3 targetPosition = freeCardPlace.GetComponent<RectTransform>().position;
                ChangeCardPlace(gap.Placed, freeCardPlace, gap, targetPosition);
            }
            while (_mainCardsPanel.HasGaps());
        }

        private void MoveCard(Card card, Vector3 targetPosition)
        {
            card.GetComponent<RectTransform>().DOMove(targetPosition, _moveBetweenPanelsTime);
        }
    }
}