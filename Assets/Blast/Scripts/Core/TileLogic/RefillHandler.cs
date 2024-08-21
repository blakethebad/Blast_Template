using System.Collections;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.TileLogic.TileStates;
using DG.Tweening;
using UnityEngine;

namespace Blast.Core.TileLogic {
    public class RefillHandler : IRefillHandler {

        private WaitForSeconds _refillDelay = new WaitForSeconds(0.05f);

        private GridMono _gridMono;
        private List<EmptyTile> _emptyTiles;
        private List<EmptyTile> _pendingEmptyTiles;

        public RefillHandler(GridMono gridMono) {
            _emptyTiles = new List<EmptyTile>();
            _pendingEmptyTiles = new List<EmptyTile>();
            _gridMono = gridMono;
        }

        public void StartGridRefill() {
            DOVirtual.DelayedCall(0.05f, Refill);

        }

        public void Refill() {
            foreach (EmptyTile emptyTile in _emptyTiles) {
                emptyTile.Refill();
            }
            
            _emptyTiles.Clear();
            _emptyTiles.AddRange(_pendingEmptyTiles);
            _pendingEmptyTiles.Clear();
            DOVirtual.DelayedCall(0.05f, Refill);
        }

        public IEnumerator RefillGrid() {

            StartGridRefill();
            yield break;
        }

        public void AddTileToRefill(Tile refillTile) {
            refillTile.SwitchToEmpty();
            EmptyTile emptyTile = ((EmptyTile)_gridMono.GetTile(refillTile.Coordinates.x, refillTile.Coordinates.y));
            _pendingEmptyTiles.Add(emptyTile);
        }
    }
}