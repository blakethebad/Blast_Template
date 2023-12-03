using System;
using System.Collections;
using Blast.Scripts.Core.Grid;
using UnityEngine;

namespace Blast.Scripts.Core.Tile.TileStates
{
    public class RefillState: BaseTileState
    {
        public override TileState State { get; protected set; } = TileState.RefillState;

        private Tile _takeItemTile;
        
        private readonly Direction[] _refillDirections = new[] { Direction.Top, Direction.TopLeft, Direction.TopRight };
        private readonly Direction[] _dropDirections = new[] { Direction.Bottom, Direction.BottomLeft, Direction.BottomRight };
        private readonly WaitForSeconds _refillTick = new WaitForSeconds(0.05f);
        
        public RefillState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }
        
        public override bool CanTranslateTo(TileState state) => state is TileState.IdleState or TileState.WaitingDropState;

        public override void EnterState(TileStatePackage tileStatePackage)
        {
            CoroutineTracker.StartCoroutine(RefillTile());
        }
        
        private IEnumerator RefillTile()
        {
            for (int i = 0; i < _refillDirections.Length; i++)
            {
                _takeItemTile = CoreTile.GetNeighbor(_refillDirections[i]);
                if(_takeItemTile == null) continue;

                //Iterate until there is an element available for drop at the take item tile
                if (_takeItemTile.IsLayerEmpty(TileLayerType.ItemLayer) && _takeItemTile.IsLayerEmpty(TileLayerType.TileAbilityLayer))
                {
                    yield return _refillTick;
                    yield return RefillTile();
                    yield break;
                }
                
                _takeItemTile.DropElement(CoreTile, () =>
                {
                    //If this is the final destination for the tile check for matches around
                    if (CoreTile.GetNeighbor(Direction.Bottom) != null && CoreTile.GetNeighbor(Direction.Bottom)
                            .CheckCurrentState(TileState.RefillState))
                        ChangeStateCallback.Invoke(new TileStatePackage(TileState.WaitingDropState));

                    else
                        ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
                });
                yield break;
            }
            
            ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
            
        }
    }
}