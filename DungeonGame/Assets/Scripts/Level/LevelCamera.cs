using UnityEngine;
using DG.Tweening;

namespace LevelLogic
{
    public class LevelCamera : MonoBehaviour
    {
        [SerializeField] private Vector2 _mainGamePosition;
        [SerializeField] private Vector2 _unitSelectPosition;
        [SerializeField] private float _animationTime;

        private void OnEnable()
        {
            Level.Instance.CurrentStateMachine.OnStateChanged += HandleLevelState;
        }

        private void OnDisable()
        {
            Level.Instance.CurrentStateMachine.OnStateChanged -= HandleLevelState;
        }

        public void HandleLevelState(LevelState state)
        {
            if (state == LevelState.MainGameState)
                transform.DOMoveX(_mainGamePosition.x, _animationTime);

        }
    }
}
