using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

namespace LevelLogic
{
    public class LevelMainGameState : LevelStateBase
    {
        private CardRouter _router;

        public void SerRouter(CardRouter router)
        {
            _router = router;
        }

        public override bool Condition()
        {
            return _router == null ? true : _router.IsAllCardPlacesCorrupted();
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