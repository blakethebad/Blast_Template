using System;
using Blast.Scripts.Core.TileElements;

namespace Blast.Scripts.Core.Grid.GridData
{
    [Serializable]
    public class ColorStoneData : TileElementData
    {
        public override BaseTileElement GenerateBase() => new ColorStone();
    }
}