﻿using System;

namespace Blast.Core.Grid.States
{
    public class GameOverState : BaseGridState
    {
        public override void EnterState(StateInfoContainer stateInfoContainer)
        {
            throw new System.NotImplementedException();
        }

        public GameOverState(Action<StateInfoContainer> changeState) : base(changeState)
        {
        }
    }
}