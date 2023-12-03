using System;

namespace Blast.Core.TileLogic.TileStates
{
    public class WaitingDropState : BaseTileState
    {
        public WaitingDropState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }

        public override TileState State { get; protected set; } = TileState.WaitingDropState;
        public override void EnterState(TileStatePackage tileStatePackage)
        {
        }

        public override bool CanTranslateTo(TileState state) => state is TileState.DropState;
    }
}