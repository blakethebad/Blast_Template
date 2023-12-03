using System;

namespace Blast.Core.TileLogic.TileStates
{
    public abstract class BaseTileState
    {
        protected Tile CoreTile { get; private set; }
        protected Action<TileStatePackage> ChangeStateCallback { get; private set; }

        public abstract TileState State { get; protected set; }
        protected BaseTileState(Tile coreTile, Action<TileStatePackage> changeStateCallback)
        {
            CoreTile = coreTile;
            ChangeStateCallback = changeStateCallback;
        }
        public abstract void EnterState(TileStatePackage tileStatePackage);

        public abstract bool CanTranslateTo(TileState state);

        public virtual void ExitState()
        {
            
        }
    }

  
}