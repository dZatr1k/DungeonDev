using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class MainCardsPanel : CardsPanel
    {
        public CardPlace GetWherePlaced(Card card)
        {
            foreach (var place in _places)
            {
                if (place.IsMyCard(card))
                    return place;
            }

            Debug.LogError("there is no card in places! Invoke IsPlacedHere before using GetWherePlaced!");
            return null;
        }

        public bool IsPlacedHere(Card card)
        {
            foreach (var place in _places)
            {
                if (place.IsMyCard(card))
                    return true;
            }
            return false;
        }

        public CardPlace GetFreeCardPlace()
        {
            foreach (var place in _places)
            {
                if (place.IsOccupied)
                    continue;
                return place;
            }

            Debug.LogError("there is no free places! Invoke IsAnyFree before using GetFreeCardPlace!");
            return null;
        }

        public bool IsAnyFree()
        {
            bool result = false;
            foreach (var place in _places)
            {
                result |= !place.IsOccupied;
            }
            return result;
        }

        public bool IsFull()
        {
            return !IsAnyFree();
        }

        public CardPlace GetGap()
        {
            CardPlace gap = null;
            foreach (var place in _places)
            {
                if (gap != null)
                {
                    if (place.IsOccupied)
                        return place;
                }
                else
                {
                    if (place.IsOccupied == false)
                        gap = place;
                }
            }
            return null;
        }

        public bool HasGaps()
        {
            bool isEmptyPlaceReached = false;
            foreach (var place in _places)
            {
                if (isEmptyPlaceReached)
                {
                    if (place.IsOccupied)
                        return true;
                }
                else
                {
                    if(place.IsOccupied == false)
                        isEmptyPlaceReached = true;
                }
            }
            return false;
        }
    }
}
