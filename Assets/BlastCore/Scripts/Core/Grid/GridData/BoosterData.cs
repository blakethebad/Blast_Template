using System;
using Match3.Tile;

namespace Match3.Grid.GridData
{
    public class BoosterData : TileElementData
    {
        public override BaseTileElement GenerateBase()
        {
            return ElementType switch
            {
                BoardElementType.HorizontalBooster => new HorizontalBooster(),
                BoardElementType.VerticalBooster => new VerticalBooster(),
                BoardElementType.PlusBooster => new PlusBooster(),
                BoardElementType.SquareBooster => new SquareBooster(),
                BoardElementType.VortexBooster => new VortexBooster(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}