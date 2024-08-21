using System;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;
using UnityEngine;

namespace Blast.Core.TileLogic
{
    public sealed class IdleTile : Tile
    {
        private readonly IMatchLocator _matchLocator;
        
        public IdleTile(Vector2Int coordinates, IRefillHandler refillHandler, IMatchLocator matchLocator, ITileFactory tileFactory) : base(coordinates, refillHandler, tileFactory) {
            _matchLocator = matchLocator;
        }

        public override void RecieveInput()
        {
            if(IsEmpty()) return;

            if (GetFirstElement() is IClickActivatable clickActivatable)
            {
                clickActivatable.Activate(null, () =>
                {
                    
                });
                return;
            }
            
            if(GetFirstElement() is not IMatchable)
                return;
            
            _matchLocator.CheckAndActivateMatch(GetFirstElement().Type, this);
        }

        public override void Activate(Match activatedMatch, Action onActivated)
        {
            if (GetFirstElement() == null || GetFirstElement() is not IActivatable activatable)
            {
                onActivated.Invoke();
                return;
            }

            activatable.Activate(activatedMatch, (() => {
                if (IsEmpty()) {
                    TileFactory.SwitchToEmpty(this);
                    RefillHandler.AddTileToRefill(this);
                }
                onActivated.Invoke();
            }));
        }

        public override void DropElement(Tile tileToDrop, Action onDropComplete)
        {
            ((IDroppable)GetFirstElement()).Drop(tileToDrop, OnDropComplete);
            if (Coordinates.y != 6) {
                SwitchToEmpty();
                RefillHandler.AddTileToRefill(this);
            }
            void OnDropComplete() {
                onDropComplete.Invoke();
            }
        }
    }
}