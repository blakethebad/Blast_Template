using System;

namespace Blast.Core.TileLogic.TileStates
{
    public class IdleState : BaseTileState
    {
        public IdleState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }

        public override TileState State { get; protected set; } = TileState.IdleState;

        public override void EnterState(TileStatePackage tileStatePackage)
        {
            
        }

        public override bool CanTranslateTo(TileState state) => true;
    }
}