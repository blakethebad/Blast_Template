using System;
using DG.Tweening;
using Match3.Grid;
using Match3.Grid.GridData;
using Match3.Tile.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Match3.Tile
{
    public class Spawner : BaseTileElement, IDroppable
    {
        public override TileLayerType Layer { get; protected set; } = TileLayerType.TileAbilityLayer;
        private bool _isDropping = false;

        private BoardElementType[] _possibleSpawnTypes = new[]
        {
            BoardElementType.RedStone, BoardElementType.BlueStone, BoardElementType.GreenStone,
            BoardElementType.YellowStone, BoardElementType.PurpleStone
        };

        public void Drop(Tile droppedTile, Action onDropComplete)
        {
            if(_isDropping)
                return;

            _isDropping = true;
            ColorStoneData newStoneData = new ColorStoneData();
            newStoneData.ElementType = _possibleSpawnTypes[Random.Range(0, _possibleSpawnTypes.Length)];

            GridMono.OnElementRequested.Invoke(newStoneData, Tile);
            
            ((IDroppable)Tile.GetFirstElement()).Drop(droppedTile, (() =>
            {
                _isDropping = false;
                onDropComplete.Invoke();
            }));
        }
    }
}