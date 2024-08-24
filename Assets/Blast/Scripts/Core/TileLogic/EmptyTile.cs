using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using UnityEngine;

namespace Blast.Core.TileLogic.TileStates
{
    public sealed class EmptyTile : Tile {
        
        public EmptyTile(Vector2Int coordinates, IRefillHandler refillHandler, ITileFactory tileFactory) : base(coordinates, refillHandler, tileFactory) {
        }

        public void Refill() {
            Tile dropTile = GetNeighbor(Direction.Top);

            if (dropTile.IsEmpty() || dropTile is EmptyTile) {
                RefillHandler.AddTileToRefill(this);
                return;
            }
            
            dropTile.DropElement(this,OnDropComplete);

            void OnDropComplete() {
                SwitchToIdle();
                Debug.LogError("switch");
            }
        }
    }
}