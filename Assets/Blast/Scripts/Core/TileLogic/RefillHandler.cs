using System.Collections;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.TileLogic.TileStates;
using DG.Tweening;
using UnityEngine;

namespace Blast.Core.TileLogic {
    public class RefillHandler : IRefillHandler {
        private readonly GridMono _gridMono;
        private readonly List<EmptyTile> _emptyTiles;
        private readonly List<EmptyTile> _pendingEmptyTiles;
        private readonly float _refillDelay = 0.05f;

        public RefillHandler(GridMono gridMono) {
            _emptyTiles = new List<EmptyTile>();
            _pendingEmptyTiles = new List<EmptyTile>();
            _gridMono = gridMono;
        }

        public void StartGridRefill() {
            DOVirtual.DelayedCall(_refillDelay, Refill);
        }

        public void Refill() {
            foreach (EmptyTile emptyTile in _emptyTiles) {
                emptyTile.Refill();
            }
            
            _emptyTiles.Clear();
            _emptyTiles.AddRange(_pendingEmptyTiles);
            _pendingEmptyTiles.Clear();
            DOVirtual.DelayedCall(_refillDelay, Refill);
        }

        public void AddTileToRefill(Tile refillTile) {
            refillTile.SwitchToEmpty();
            EmptyTile emptyTile = ((EmptyTile)_gridMono.GetTile(refillTile.Coordinates.x, refillTile.Coordinates.y));
            _pendingEmptyTiles.Add(emptyTile);
        }
    }
}