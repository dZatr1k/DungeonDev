using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelLogic
{
    public class StateMachineFailedHandler : MonoBehaviour
    {
        [SerializeField] private LevelStateMachine _stateMachineToHandle;
        [SerializeField] private GameObject _mainGameWarningPanel;

        private void OnEnable()
        {
            _stateMachineToHandle.OnFailedToChangeState += HandleFail;
        }

        private void OnDisable()
        {
            _stateMachineToHandle.OnFailedToChangeState -= HandleFail;
        }

        private void HandleFail(LevelState state)
        {
            switch (state)
            {
                case LevelState.MainGameState:
                    HandleFailOfMainGameState();
                    break;
            }
        }

        private void HandleFailOfMainGameState()
        {
            _mainGameWarningPanel.SetActive(true);
        }
    }
}
