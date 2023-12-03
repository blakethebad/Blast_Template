namespace Blast.Core.Grid
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