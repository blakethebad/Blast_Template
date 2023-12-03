using System;
using System.Collections.Generic;
using Match3.Grid;
using Match3.Grid.Match;
using Match3.Tile.Interfaces;
using Match3.Tile.TileStates;
using UnityEngine;

namespace Match3.Tile
{
    public sealed class Tile
    {
        public Vector2Int Coordinates { get; private set; }
        public Vector3 WorldPosition { get; private set; }

        private BaseTileState _currentState;
        private readonly TileMatchChecker _matchChecker = new ();

        private Dictionary<Direction, Tile> _neighbors;
        private readonly Dictionary<TileLayerType, ITileLayer> _layers;
        private readonly Dictionary<TileState, BaseTileState> _states;

        public Tile(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            WorldPosition = new Vector3(Coordinates.x, Coordinates.y);
            _matchChecker.InitMatchChecker(this);
            _layers = new Dictionary<TileLayerType, ITileLayer>()
            {
                { TileLayerType.BlockerLayer, new BlockerLayer() },
                { TileLayerType.ItemLayer, new ItemLayer() },
                { TileLayerType.TileAbilityLayer, new TileAbilityLayer() },
                { TileLayerType.TileBackgroundLayer, new TileBackgroundLayer() },
            };
            _states = new Dictionary<TileState, BaseTileState>()
            {
                { TileState.IdleState, new IdleState(this, ChangeState) },
                { TileState.ActivateState, new ActivateState(this, ChangeState) },
                { TileState.DropState, new DropState(this, ChangeState) },
                { TileState.RefillState, new RefillState(this, ChangeState) },
                { TileState.RecieveInputState, new RecieveInputState(this, ChangeState) },
                { TileState.SwapState, new SwapState(this, ChangeState)},
                { TileState.WaitingDropState , new WaitingDropState(this, ChangeState)}
            };
            _currentState = _states[TileState.IdleState];
        }

        #region Neighbor

        public void FindNeighbors(GridMono grid)
        {
            _neighbors = new Dictionary<Direction, Tile>
            {
                { Direction.None, null},
                { Direction.Left , grid.GetTile(Coordinates.x - 1, Coordinates.y)},
                { Direction.TopLeft, grid.GetTile(Coordinates.x - 1, Coordinates.y + 1) },
                { Direction.Top, grid.GetTile(Coordinates.x, Coordinates.y + 1) },
                { Direction.TopRight, grid.GetTile(Coordinates.x + 1, Coordinates.y + 1) },
                { Direction.Right, grid.GetTile(Coordinates.x + 1, Coordinates.y) },
                { Direction.BottomRight, grid.GetTile(Coordinates.x + 1, Coordinates.y - 1) },
                { Direction.Bottom, grid.GetTile(Coordinates.x, Coordinates.y - 1) },
                { Direction.BottomLeft, grid.GetTile(Coordinates.x - 1, Coordinates.y - 1) }
            };
        }

        public Tile GetNeighbor(Direction direction) => _neighbors[direction];

        #endregion

        #region TileElements

        public bool IsEmpty()
        {
            foreach (ITileLayer layer in _layers.Values)
            {
                if (layer.IsEmpty())
                    continue;
                return false;
            }
            return true;
        }

        public BaseTileElement GetFirstElement()
        {
            foreach (ITileLayer layer in _layers.Values)
            {
                if(layer.IsEmpty())
                    continue;
                return layer.GetFirstElement();
            }
            return null;
        }

        public void AddTileElement(BaseTileElement tileElement)
        {
            _layers[tileElement.Layer].AddTileElement(tileElement);
            tileElement.SetTile(this);
        }
        
        public bool IsLayerEmpty(TileLayerType layerType) => _layers[layerType].IsEmpty();
        public bool LayerContainsType(TileLayerType layerType, BoardElementType type) => _layers[layerType].LayerContainsType(type);
        public void RemoveElementFromTile(BaseTileElement element) => _layers[element.Layer].RemoveElement(element);
        public BoardElementType GetFirstTypeFromLayer(TileLayerType layerType) => _layers[layerType].GetFirstType();


        #endregion
        
        #region TileActions

        private void ChangeState(TileStatePackage statePackage)
        {
            if (!_currentState.CanTranslateTo(statePackage.StateToTranslate)) return;
            _currentState = _states[statePackage.StateToTranslate];
            _currentState.EnterState(statePackage);
        }
        
        public bool CheckCurrentState(TileState state) => _currentState.State == state;
        
        public void Swap(Direction swapDirection, ISwappable swappedElement, bool isUndo){}

        public void RefillTile() => ChangeState(new TileStatePackage(TileState.RefillState));
        
        public void DropElement(Tile tileToDrop, Action onDropComplete) => ChangeState(new TileStatePackage(TileState.DropState, tileToDrop, onDropComplete));

        public void RecieveInput(Direction inputDirection) => ChangeState(new TileStatePackage(TileState.RecieveInputState, inputDirection));

        public void Activate(Match activatedMatch, Action onTileActivated) => ChangeState(new TileStatePackage(TileState.ActivateState, activatedMatch, onTileActivated));

        public bool CheckAndActivateMatch() => _matchChecker.CheckMatch();


        #endregion
    }
}