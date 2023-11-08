using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

namespace LevelLogic
{
    public class LevelMainGameState : LevelStateBase
    {
        private readonly MainCardsPanel _mainPanel;

        public LevelMainGameState(MainCardsPanel mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public override bool Condition()
        {
            return _mainPanel == null ? true : _mainPanel.IsFull();
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}