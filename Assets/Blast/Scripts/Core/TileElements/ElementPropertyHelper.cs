using System;
using Blast.Scripts.Core.Grid;
using DG.Tweening;

namespace Blast.Scripts.Core.TileElements
{
    public class ElementPropertyHelper
    {
        public void Swap(BaseTileElement baseTileElement, Tile.Tile tile, Direction direction)
        {
            tile.GetNeighbor(direction).AddTileElement(baseTileElement);
            //Animation added later
            baseTileElement.Transform.position = tile.GetNeighbor(direction).WorldPosition;
        }

        public void Drop(BaseTileElement baseTileElement, Tile.Tile elementTile, Tile.Tile droppedTile, Action onDropComplete)
        {
            Tile.Tile testTile = elementTile;
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