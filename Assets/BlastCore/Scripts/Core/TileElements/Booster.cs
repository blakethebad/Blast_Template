using System;
using System.Collections.Generic;
using DG.Tweening;
using Match3.Grid;
using Match3.Grid.Match;
using Match3.Tile.Interfaces;
using UnityEngine;

namespace Match3.Tile
{
    public abstract class Booster : BaseTileElement, IDroppable, ISwappable, IBooster, IClickActivatable
    {
        private List<BoardElementType> _boosterPriority = new List<BoardElementType>()
        {
            BoardElementType.HorizontalBooster,
            BoardElementType.VerticalBooster,
            BoardElementType.SquareBooster,
            BoardElementType.PlusBooster,
            BoardElementType.VortexBooster
        };
        
        public override TileLayerType Layer { get; protected set; } = TileLayerType.ItemLayer;

        public abstract void Activate(Match activatedMatch, Action onActivationComplete);

        public abstract void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated);

        public abstract void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated);

        public void Swap(Direction direction) => _propertyHelper.Swap(this, Tile, direction);

        public void Drop(Tile droppedTile, Action onDropComplete) => _propertyHelper.Drop(this, Tile, droppedTile, onDropComplete);
    }
}