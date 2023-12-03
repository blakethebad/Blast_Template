using System.Collections.Generic;
using Blast.Core.TileElements;

namespace Blast.Core.TileLogic.Layers
{
    public class BlockerLayer : ITileLayer
    {
        private List<BaseTileElement> _blockers = new List<BaseTileElement>();

        public BaseTileElement GetFirstElement()
        {
            if (IsEmpty())
                return null;

            return _blockers[0];
        }

        public void AddTileElement(BaseTileElement tileElement)
        {
            _blockers.Add(tileElement);
        }

        public void RemoveElement(BaseTileElement tileElement)
        {
            
        }

        public bool IsEmpty()
        {
            return _blockers.Count == 0;
        }

        public bool LayerContainsType(BoardElementType type)
        {
            return false;
        }

        public BoardElementType GetFirstType()
        {
            if (IsEmpty())
                return BoardElementType.None;

            return _blockers[0].Type;
        }
    }
}