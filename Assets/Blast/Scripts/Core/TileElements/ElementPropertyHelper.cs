using System;
using Blast.Core.Grid;
using Blast.Core.TileLogic;
using DG.Tweening;

namespace Blast.Core.TileElements
{
    public class ElementPropertyHelper
    {
        public void Swap(BaseTileElement baseTileElement, Tile tile, Direction direction)
        {
            tile.GetNeighbor(direction).AddTileElement(baseTileElement);
            //Animation added later
            baseTileElement.Transform.position = tile.GetNeighbor(direction).WorldPosition;
        }

        public void Drop(BaseTileElement baseTileElement, Tile elementTile, Tile droppedTile, Action onDropComplete)
        {
            Tile testTile = elementTile;
            elementTile.RemoveElementFromTile(baseTileElement);
            float baseDuration = 0.09f;
            baseTileElement.Transform.DOMove(droppedTile.WorldPosition, baseDuration).OnComplete((() =>
            {
                droppedTile.AddTileElement(baseTileElement);
                onDropComplete.Invoke();
            }));
        }
    }
}