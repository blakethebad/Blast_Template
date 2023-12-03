using System;
using DG.Tweening;
using Match3.Grid;
using UnityEngine;

namespace Match3.Tile
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
            Match3.Tile.Tile testTile = elementTile;
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