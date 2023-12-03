using System;

namespace Blast.Core.Grid.States
{
    public class WinFailState : BaseGridState
    {
        public override void EnterState(StateInfoContainer stateInfoContainer)
        {
            throw new System.NotImplementedException();
        }

        public WinFailState(Action<StateInfoContainer> changeState) : base(changeState)
        {
        }
    }
}