using System;

namespace Match3.Tile.TileStates
{
    public class SwapState : BaseTileState
    {
        public SwapState(Tile _coreTile, Action<TileStatePackage> _changeStateCallback) : base(_coreTile, _changeStateCallback)
        {
        }

        public override TileState State { get; protected set; } = TileState.SwapState;

        public override void EnterState(TileStatePackage tileStatePackage)
        {
            throw new NotImplementedException();
        }

        public override bool CanTranslateTo(TileState state)
        {
            return state is TileState.ActivateState or TileState.IdleState;
        }
    }
}