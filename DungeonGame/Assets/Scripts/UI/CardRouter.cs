using UnityEngine;
using DG.Tweening;
using LevelLogic;

namespace Card
{
    public class CardRouter : MonoBehaviour
    {
        [SerializeField] private float _moveTime;
        [SerializeField] private MainCardsPanel _mainPanel;
        [SerializeField] private CardsListPanel _listPanel;

        private ICardRouterBehaviour _behaviour;

        public float MoveTime => _moveTime;
        public MainCardsPanel MainPanel => _mainPanel;
        public CardsListPanel ListPanel => _listPanel;

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
                    _behaviour = new PreGameCardRouterBehaviour();
                    break;
                case LevelState.MainGameState:
                    _behaviour = new MainGameCardRouterBehaviour();
                    break;
            }
        }

        private void OnCardClick(Card card)
        {
            _behaviour.OnClick(this, card);
        }

        public bool IsAllCardPlacesCorrupted()
        {
            return _mainPanel.IsFull();
        }
    }
}