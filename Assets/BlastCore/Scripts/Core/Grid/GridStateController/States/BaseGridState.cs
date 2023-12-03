﻿using System;
using UnityEngine;

namespace Match3.Grid.StateController
{
    public abstract class BaseGridState
    {
        protected Action<StateInfoContainer> ChangeState { get; private set; }
        
        protected BaseGridState(Action<StateInfoContainer> changeState)
        {
            ChangeState = changeState;
        }
        
        public abstract void EnterState(StateInfoContainer stateInfoContainer);

        public virtual void ExitState(StateInfoContainer stateInfoContainer)
        {
        }

        public virtual bool CanChangeState(StateInfoContainer stateInfoContainer)
        {
            return true;
        }
    }
}