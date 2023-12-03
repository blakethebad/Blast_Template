using Blast.Core.TileElements;

namespace Blast.Core.TileLogic.Layers
{
    public class ItemLayer : ITileLayer
    {
        private BaseTileElement _item;

        public BaseTileElement GetFirstElement() => _item;

        public void AddTileElement(BaseTileElement tileElement) => _item = tileElement;
        public void RemoveElement(BaseTileElement tileElement)
        {
            _item = null;
            tileElement.SetTile(null);
        }

        public bool IsEmpty() => _item == null;
        public bool LayerContainsType(BoardElementType type) => !IsEmpty() && _item.Type == type;
        public BoardElementType GetFirstType()
        {
            if (IsEmpty())
                return BoardElementType.None;

            return _item.Type;
        }
    }
}