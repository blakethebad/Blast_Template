using System;
using Match3.Tile;

namespace Match3.Grid.GridData
{
    [Serializable]
    public class ColorStoneData : TileElementData
    {
        public override BaseTileElement GenerateBase() => new ColorStone();
    }
}