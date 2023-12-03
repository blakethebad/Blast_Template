using System;
using System.Collections;
using Match3.Grid;
using Match3.Grid.Match;

namespace Match3.Tile.TileStates
{
    public struct TileStatePackage
    {
        public TileState StateToTranslate { get; private set; }
        
        //Recive Input State
        public Direction InputDirection { get; private set; }
        
        //ActivateState
        public Match ActivatedMatch { get; private set; }
        public Action OnTileActivated { get; private set; }
        
        //DropState
        public Tile TileToDrop { get; private set; }
        public Action OnDropComplete { get; private set; }
        
        //RefillState

        //Idle State and Refill State
        public TileStatePackage(TileState stateToTranslate)
        {
            StateToTranslate = stateToTranslate;
            InputDirection = Direction.None;
            ActivatedMatch = null;
            TileToDrop = null;
            OnDropComplete = null;
            OnTileActivated = null;
        }

        //RecieveInputState
        public TileStatePackage(TileState stateToTranslate, Direction inputDirection)
        {
            StateToTranslate = stateToTranslate;
            InputDirection = inputDirection;
            ActivatedMatch = null;
            TileToDrop = null;
            OnDropComplete = null;
            OnTileActivated = null;
        }
        
        //ActivateState
        public TileStatePackage(TileState stateToTranslate, Match activatedMatch, Action onTileActivated)
        {
            StateToTranslate = stateToTranslate;
            ActivatedMatch = activatedMatch;
            InputDirection = Direction.None;
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
            InputDirection = Direction.None;
            OnTileActivated = null;
        }
    }
}