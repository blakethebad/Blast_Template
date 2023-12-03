using System;

namespace Match3.Grid.StateController
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