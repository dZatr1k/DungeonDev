using UnityEngine;
using DG.Tweening;
using LevelLogic;
using GameBoard;

namespace Card
{
    public class CardRouter : MonoBehaviour
    {
        [Header("Panels config")]
        [SerializeField] private MainCardsPanel _mainPanel;
        [SerializeField] private CardsListPanel _listPanel;

        [Header("Heroes config")]
        [SerializeField] private HeroPlacer _heroPlacer;
        
        [Header("CardSwapper config")]
        [SerializeField] private float _moveBetweenPanelsTime;

        private ICardBehaviour _behaviour;

        private void OnEnable()
        {
            Level.Instance.CurrentStateMachine.OnStateChanged += ChangeCardBehaviour;
            Card.OnClick += OnCardClick;
        }

        private void OnDisable()
        {
            Level.Instance.CurrentStateMachine.OnStateChanged += ChangeCardBehaviour;
            Card.OnClick -= OnCardClick;
        }

        private void ChangeCardBehaviour(LevelState state)
        {
            switch (state)
            {
                case LevelState.UnitSelectState:
                    _behaviour = new CardSwapper(_mainPanel, _listPanel, _moveBetweenPanelsTime);
                    break;
                case LevelState.MainGameState:
                    _behaviour = new CardSelector(_heroPlacer);
                    break;
            }
        }

        private void OnCardClick(Card card)
        {
            _behaviour.OnClick(card);
        }
    }
}