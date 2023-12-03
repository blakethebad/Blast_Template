using Blast.Scripts.Core.TileElements;

namespace Blast.Scripts.Core.Tile.Layers
{
    public interface ITileLayer
    {
        public BaseTileElement GetFirstElement();
        public void AddTileElement(BaseTileElement tileElement);
        public void RemoveElement(BaseTileElement tileElement);
        public bool IsEmpty();
        public bool LayerContainsType(BoardElementType type);
        public BoardElementType GetFirstType();
    }
}