using System.Collections;

namespace Blast.Core.TileLogic {
    public interface IRefillHandler {
        void StartGridRefill();
        public void AddTileToRefill(Tile refillTile);
    }
}