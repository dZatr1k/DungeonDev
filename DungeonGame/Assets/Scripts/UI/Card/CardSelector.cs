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
            HeroPlacer.OnHeroPlaced += Deselect;
        }

        public void Deselect(Cell cell)
        {
            OnHeroPlaced();
        }

        private void OnHeroPlaced()
        {
            _lastSelected.CardView.HideBuyHaze();
            _lastSelected.Deactivate();
            _heroPlacer.ResetHero();
            _lastSelected = null;
        }

        public void OnClick(Card card)
        {
            if(card == _lastSelected)
            {
                _lastSelected.CardView.DisableHaze();
                _heroPlacer.ResetHero();
                _lastSelected = null;
            }
            else
            {
                if (_heroPlacer.SetHeroToPlace(card))
                {
                    if(_lastSelected != null)
                        _lastSelected.CardView.DisableHaze();
                    _lastSelected = card;
                    card.CardView.EnableHaze();
                }
                else
                {
                    card.CardView.PlayError();
                }
            }
        }
    }
}
