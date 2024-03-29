﻿using Blast.Core.TileElements;

namespace Blast.Core.TileLogic.Layers
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