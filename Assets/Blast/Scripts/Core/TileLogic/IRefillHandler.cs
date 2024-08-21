using System.Collections;

namespace Blast.Core.TileLogic {
    public interface IRefillHandler {
        IEnumerator RefillGrid();
        public void AddTileToRefill(Tile refillTile);
    }
}