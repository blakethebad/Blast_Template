using System;
using Blast.Core.Grid;
using Blast.Core.Grid.GridData;
using Blast.Core.TileElements.Interfaces;
using Blast.Core.TileLogic;
using Random = UnityEngine.Random;

namespace Blast.Core.TileElements
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