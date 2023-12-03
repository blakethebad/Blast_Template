using System;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.Grid.GridData;
using Blast.Scripts.Core.TileElements.Interfaces;
using Random = UnityEngine.Random;

namespace Blast.Scripts.Core.TileElements
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

        public void Drop(Tile.Tile droppedTile, Action onDropComplete)
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