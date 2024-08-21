using Blast.Core.Grid.GridData;
using Blast.Core.TileLogic;

namespace Blast.Core.Grid.Factories {
    public interface ITileElementFactory
    {
        void CreateElementOnTile(TileElementData elementData, Tile createdTile);
    }
}