using System;

namespace Blast.Scripts.Core.Grid.GridStateController.States
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