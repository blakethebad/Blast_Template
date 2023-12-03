using System;
using Blast.Scripts.Core.TileElements.Interfaces;

namespace Blast.Scripts.Core.Tile.TileStates
{
    public class DropState: BaseTileState
    {
        public override TileState State { get; protected set; } = TileState.DropState;

        public DropState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }


        public override void EnterState(TileStatePackage tileStatePackage)
        {
            //Drop the element on take item tile to core tile
            ((IDroppable)CoreTile.GetFirstElement()).Drop(tileStatePackage.TileToDrop, () =>
            {
                tileStatePackage.OnDropComplete.Invoke();
                CoreTile.RefillTile();
            });
        }

        public override bool CanTranslateTo(TileState state) => state is TileState.RefillState;
    }
}