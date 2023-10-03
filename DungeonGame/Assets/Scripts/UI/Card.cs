using UnityEngine;
using UnityEngine.EventSystems;
using Level;

namespace Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        private ICardBehaviour _currentCardBehaviour;

        private void OnEnable()
        {
            LevelStateMachine.OnStateChanged += HandleState;
        }

        private void OnDisable()
        {
            LevelStateMachine.OnStateChanged += HandleState;
        }

        private void HandleState(LevelState state)
        {
            if (state == LevelState.UnitSelectState)
            {
                _currentCardBehaviour = new PreGameCardBehaviour();
            }
            else if(state == LevelState.MainGameState)
            {
                _currentCardBehaviour = new MainGameCardBehaviour();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_currentCardBehaviour != null)
            {
                _currentCardBehaviour.Click(this);
            }
        }
    }
}