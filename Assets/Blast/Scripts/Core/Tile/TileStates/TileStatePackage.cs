using System;
using Blast.Scripts.Core.Grid;

namespace Blast.Scripts.Core.Tile.TileStates
{
    public struct TileStatePackage
    {
        public TileState StateToTranslate { get; private set; }
        
        //ActivateState
        public Match.Match ActivatedMatch { get; private set; }
        public Action OnTileActivated { get; private set; }
        
        //DropState
        public Tile TileToDrop { get; private set; }
        public Action OnDropComplete { get; private set; }
        
        //RefillState

        //Idle State and Refill State
        public TileStatePackage(TileState stateToTranslate)
        {
            StateToTranslate = stateToTranslate;
            ActivatedMatch = null;
            TileToDrop = null;
            OnDropComplete = null;
            OnTileActivated = null;
        }

        //RecieveInputState
        public TileStatePackage(TileState stateToTranslate, Direction inputDirection)
        {
            StateToTranslate = stateToTranslate;
            ActivatedMatch = null;
            TileToDrop = null;
            OnDropComplete = null;
            OnTileActivated = null;
        }
        
        //ActivateState
        public TileStatePackage(TileState stateToTranslate, Match.Match activatedMatch, Action onTileActivated)
        {
            StateToTranslate = stateToTranslate;
            ActivatedMatch = activatedMatch;
            TileToDrop = null;
            OnDropComplete = null;
            OnTileActivated = onTileActivated;
        }

        //Drop State
        public TileStatePackage(TileState stateToTranslate, Tile tileToDrop, Action onDropComplete)
        {
            StateToTranslate = stateToTranslate;
            TileToDrop = tileToDrop;
            OnDropComplete = onDropComplete;
            ActivatedMatch = null;
            OnTileActivated = null;
        }
    }
}