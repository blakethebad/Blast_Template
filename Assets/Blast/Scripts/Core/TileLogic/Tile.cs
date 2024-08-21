using System;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements;
using Blast.Core.TileLogic.Layers;
using UnityEngine;

namespace Blast.Core.TileLogic
{
    public abstract class Tile {
        public Vector2Int Coordinates { get; private set; }
        public Vector3 WorldPosition { get; private set; }

        private Dictionary<Direction, Tile> _neighbors;
        private  Dictionary<TileLayerType, ITileLayer> _layers;
        protected readonly IRefillHandler RefillHandler;
        protected readonly ITileFactory TileFactory;
        
        public Tile(Vector2Int coordinates, IRefillHandler refillHandler, ITileFactory tileFactory) {
            RefillHandler = refillHandler;
            TileFactory = tileFactory;
            Coordinates = coordinates;
            WorldPosition = new Vector3(Coordinates.x, Coordinates.y);
            _layers = new Dictionary<TileLayerType, ITileLayer>()
            {
                { TileLayerType.BlockerLayer, new BlockerLayer() },
                { TileLayerType.ItemLayer, new ItemLayer() },
                { TileLayerType.TileAbilityLayer, new TileAbilityLayer() },
                { TileLayerType.TileBackgroundLayer, new TileBackgroundLayer() },
            };
        }

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
        public void RemoveElementFromTile(BaseTileElement element) => _layers[element.Layer].RemoveElement(element);

        public virtual void DropElement(Tile tileToDrop, Action onDropComplete) => onDropComplete.Invoke();

        public virtual void RecieveInput() { }

        public virtual void Activate(Match activatedMatch, Action onActivated) => onActivated.Invoke();

        public void SwitchToIdle() => TileFactory.SwitchToIdle(this);
        public void SwitchToEmpty() => TileFactory.SwitchToEmpty(this);

        public void SetNeighbors(Dictionary<Direction, Tile> neighbors) {
            _neighbors = neighbors;
        }

        public Dictionary<Direction, Tile> GetNeighbors() => _neighbors;

        public void SetLayers(Dictionary<TileLayerType, ITileLayer> layers) {
            _layers = layers;
        }

        public Dictionary<TileLayerType, ITileLayer> GetLayers() => _layers;

    }
}