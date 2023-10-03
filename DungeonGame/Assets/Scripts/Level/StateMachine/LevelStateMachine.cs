using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    [RequireComponent(typeof(Level))]
    public class LevelStateMachine : MonoBehaviour
    {
        public static UnityAction<LevelState> OnStateChanged;

        private readonly LevelStateBase _unitSelectState = new UnitSelectState();
        private readonly LevelMainGameState _mainGameState = new LevelMainGameState();

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
            ChangeState(LevelState.UnitSelectState);
        }

        private void Update()
        {
            _currentState.Update();
        }

        public void ChangeState(int newState)
        {
            ChangeState((LevelState)newState);
        }

        public void ChangeState(LevelState newState)
        {
            if (LevelStateBaseByLevelState.ContainsKey(newState) == false)
            {
                Debug.LogException(new ArgumentException($"Dictionary doesn't contains {newState}\n" +
                    $"Maybe you forgot add this state to dictionary."));
            }

            if(_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = LevelStateBaseByLevelState[newState];
            _currentState.Enter();

            OnStateChanged?.Invoke(newState);

            Debug.Log("Current Level state is " + newState.ToString());
        }
    }
}