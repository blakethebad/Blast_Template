using System;

namespace Blast.Scripts.Core.Grid.GridStateController.States
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