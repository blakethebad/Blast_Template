namespace Match3.Grid.StateController
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