using UnityEngine;
using GameBoard;

namespace Card
{
    public class CardSelector : ICardBehaviour
    {
        private readonly HeroPlacer _heroPlacer;

        private Card _lastSelected;

        public CardSelector(HeroPlacer heroPlacer)
        {
            _heroPlacer = heroPlacer;
        }

        public void OnClick(Card card)
        {
            if(_lastSelected != null)
            {
                _lastSelected.CardView.DisableHaze();
                _heroPlacer.SetHeroToPlace(null);
                _lastSelected = null;
            }
            else if(_lastSelected != card)
            {
                _lastSelected = card;
                _heroPlacer.SetHeroToPlace(card.Hero);
                card.CardView.EnableHaze();
            }
        }
    }
}
