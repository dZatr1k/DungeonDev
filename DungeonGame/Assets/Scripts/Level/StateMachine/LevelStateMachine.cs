using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Card;

namespace LevelLogic
{
    [RequireComponent(typeof(Level))]
    public class LevelStateMachine : MonoBehaviour
    {
        [SerializeField] private CardRouter _router;

        public UnityAction<LevelState> OnStateChanged;
        public UnityAction OnGameStarted;
        public UnityAction<LevelState> OnFailedToChangeState;

        private readonly LevelStateBase _unitSelectState = new UnitSelectState();
        private LevelMainGameState _mainGameState = new LevelMainGameState();

        private LevelStateBase _currentState;
        private Dictionary<LevelState, LevelStateBase> LevelStateBaseByLevelState;

        private void InitializeDictionary()
        {
            LevelStateBaseByLevelState = new Dictionary<LevelState, LevelStateBase>();
            LevelStateBaseByLevelState.Add(LevelState.UnitSelectState, _unitSelectState);
            LevelStateBaseByLevelState.Add(LevelState.MainGameState, _mainGameState);
        }

        private void Start()
        {
            InitializeDictionary();
            _mainGameState.SerRouter(_router);
            ChangeState(LevelState.UnitSelectState);
        }

        private void Update()
        {
            _currentState.Update();
        }

        public void ChangeState(LevelState newState)
        {
            if (LevelStateBaseByLevelState.ContainsKey(newState) == false)
            {
                Debug.LogException(new ArgumentException($"Dictionary doesn't contains {newState}\n" +
                    $"Maybe you forgot add this state to dictionary."));
            }

            if (LevelStateBaseByLevelState[newState].Condition() == false)
            {
                OnFailedToChangeState?.Invoke(newState);
                return;
            }

            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = LevelStateBaseByLevelState[newState];
            _currentState.Enter();

            OnStateChanged?.Invoke(newState);
            if (newState == LevelState.MainGameState)
                OnGameStarted?.Invoke();

            Debug.Log("Current Level state is " + newState.ToString());
        }
    }
}