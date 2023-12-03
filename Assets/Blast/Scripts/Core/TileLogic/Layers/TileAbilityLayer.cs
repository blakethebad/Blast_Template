using Blast.Core.TileElements;

namespace Blast.Core.TileLogic.Layers
{
    public class TileAbilityLayer : ITileLayer
    {
        private BaseTileElement _tileAbility;

        public BaseTileElement GetFirstElement() => _tileAbility;

        public void AddTileElement(BaseTileElement tileElement) => _tileAbility = tileElement;
        public void RemoveElement(BaseTileElement tileElement)
        {
            _tileAbility = null;
        }

        public bool IsEmpty() => _tileAbility == null;
        public bool LayerContainsType(BoardElementType type)
        {
            return false;
        }

        public BoardElementType GetFirstType()
        {
            if (IsEmpty())
                return BoardElementType.None;

            return _tileAbility.Type;
        }
    }
}