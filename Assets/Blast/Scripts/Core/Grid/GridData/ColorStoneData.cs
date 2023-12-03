using System;
using Blast.Core.TileElements;

namespace Blast.Core.Grid.GridData
{
    [Serializable]
    public class ColorStoneData : TileElementData
    {
        public override BaseTileElement GenerateBase() => new ColorStone();
    }
}