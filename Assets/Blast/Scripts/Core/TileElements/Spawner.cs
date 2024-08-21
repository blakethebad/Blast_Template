using System;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
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
        private ITileElementFactory _tileElementFactory;
        private BoardElementType[] _possibleSpawnTypes = new[]
        {
            BoardElementType.RedStone, BoardElementType.BlueStone, BoardElementType.GreenStone,
            BoardElementType.YellowStone, BoardElementType.PurpleStone
        };

        public Spawner(ITileElementFactory tileElementFactory) {
            _tileElementFactory = tileElementFactory;
        }

        public void Drop(Tile droppedTile, Action onDropComplete)
        {
            if(_isDropping)
                return;

            _isDropping = true;
            TileElementData newStoneData = new TileElementData();
            newStoneData.ElementType = _possibleSpawnTypes[Random.Range(0, _possibleSpawnTypes.Length)];

            _tileElementFactory.CreateElementOnTile(newStoneData, Tile);
            
            ((IDroppable)Tile.GetFirstElement()).Drop(droppedTile, (() =>
            {
                _isDropping = false;
                onDropComplete.Invoke();
            }));
        }
    }
}