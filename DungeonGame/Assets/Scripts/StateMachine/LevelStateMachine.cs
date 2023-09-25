using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class LevelStateMachine : MonoBehaviour
    {
        public static UnityAction<LevelState> OnStateChanged;

        private readonly LevelStateBase _unitSelectState = new UnitSelectState();

        private LevelStateBase _currentState;
        private Dictionary<LevelState, LevelStateBase> LevelStateBaseByLevelState;

        private void InitializeDictionary()
        {
            LevelStateBaseByLevelState.Add(LevelState.UnitSelectState, _unitSelectState);
        }

        private void Awake()
        {
            InitializeDictionary();
        }

        private void Start()
        {
            _currentState = _unitSelectState;
            _currentState.Enter();
        }

        private void Update()
        {
            _currentState.Update();
        }

        public void ChangeState(LevelState newState)
        {
            if (LevelStateBaseByLevelState.ContainsKey(newState))
            {
                Debug.LogException(new ArgumentException($"Dictionary doesn't contains {newState.ToString()}\n" +
                    $"Maybe you forgot add this state to dictionary."));
            }

            _currentState.Exit();
            _currentState = LevelStateBaseByLevelState[newState];
            _currentState.Enter();

            OnStateChanged?.Invoke(newState);
        }
    }
}