using System;
using System.Threading.Tasks;

namespace Blast.Scripts.Core.Grid.GridStateController.States
{
    public class DelayInputState : BaseGridState
    {
        public DelayInputState(Action<StateInfoContainer> changeState) : base(changeState)
        {
        }

        public override void EnterState(StateInfoContainer stateInfoContainer)
        {
            DelayInput();
        }

        private async void DelayInput()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.2f));
            ChangeState.Invoke(new StateInfoContainer(GridState.InputState));
        }
    }
}