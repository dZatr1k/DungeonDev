using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Card
{
    public class PreGameCardRouterBehaviour : ICardRouterBehaviour
    {
        public void OnClick(CardRouter router, Card card)
        {
            if (router.MainPanel.IsPlacedHere(card))
            {
                var whereCardPlaced = router.MainPanel.GetWherePlaced(card);
                var targetCardPlace = router.ListPanel.GetPlaceBy(card.ID);
                Vector3 targetPosition = router.ListPanel.GetPositionBy(card.ID);
                ChangeCardPlace(router, card, targetCardPlace, whereCardPlaced, targetPosition);

                CheckMainPanelGaps(router);
            }
            else
            {
                if (router.MainPanel.IsAnyFree())
                {
                    var freeCardPlace = router.MainPanel.GetFreeCardPlace();
                    var currentCardPlace = router.ListPanel.GetPlaceBy(card.ID);
                    Vector3 targetPosition = freeCardPlace.GetComponent<RectTransform>().position;
                    ChangeCardPlace(router, card, freeCardPlace, currentCardPlace, targetPosition);
                }
            }
        }

        private void ChangeCardPlace(CardRouter router, Card card, CardPlace to, CardPlace from, Vector3 targetPosition)
        {
            to.Place(card);
            from.Free();

            card.transform.SetParent(to.transform);
            MoveCard(router, card, targetPosition);
        }

        private void CheckMainPanelGaps(CardRouter router)
        {
            if (router.MainPanel.HasGaps())
                FixMainPanelGaps(router);
        }

        private void FixMainPanelGaps(CardRouter router)
        {
            do
            {
                var freeCardPlace = router.MainPanel.GetFreeCardPlace();
                var gap = router.MainPanel.GetGap();
                Vector3 targetPosition = freeCardPlace.GetComponent<RectTransform>().position;
                ChangeCardPlace(router, gap.Placed, freeCardPlace, gap, targetPosition);
            }
            while (router.MainPanel.HasGaps());
        }

        private void MoveCard(CardRouter router, Card card, Vector3 targetPosition)
        {
            card.GetComponent<RectTransform>().DOMove(targetPosition, router.MoveTime);
        }
    }
}

