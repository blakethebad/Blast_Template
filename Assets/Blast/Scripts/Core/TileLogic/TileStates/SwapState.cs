using System;

namespace Blast.Core.TileLogic.TileStates
{
    public class SwapState : BaseTileState
    {
        public SwapState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
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