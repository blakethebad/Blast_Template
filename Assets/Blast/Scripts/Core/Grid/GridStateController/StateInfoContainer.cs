namespace Blast.Scripts.Core.Grid.GridStateController
{
    public struct StateInfoContainer
    {
        public GridState State { get; private set; }

        public StateInfoContainer(GridState state)
        {
            State = state;
        }
    }
}