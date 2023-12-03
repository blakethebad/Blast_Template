using System;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;

namespace Blast.Core.TileLogic.TileStates
{
    public class RecieveInputState : BaseTileState
    {
        public override TileState State { get; protected set; } = TileState.RecieveInputState;
        private readonly Queue<Tile> _searchQueue = new Queue<Tile>();
        private readonly HashSet<Tile> _searchedTiles = new HashSet<Tile>();
        private readonly HashSet<Tile> _matchedTiles = new HashSet<Tile>();
        private readonly Direction[] _searchDirections = new [] { Direction.Top , Direction.Bottom, Direction.Right, Direction.Left};

        public RecieveInputState(Tile coreTile, Action<TileStatePackage> changeStateCallback) : base(coreTile, changeStateCallback)
        {
        }
        
        public override bool CanTranslateTo(TileState state) => state is TileState.ActivateState or TileState.IdleState;

        public override void EnterState(TileStatePackage tileStatePackage)
        {
            if(CoreTile.IsEmpty()) return;

            if (CoreTile.GetFirstElement() is IClickActivatable clickActivatable)
            {
                clickActivatable.Activate(null, () =>
                {
                    ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
                });
                return;
            }

            ChangeStateCallback.Invoke(new TileStatePackage(TileState.IdleState));
            
            if(CoreTile.GetFirstElement() is not IMatchable)
                return;
            
            Match foundMatch = CheckMatch();
            if (foundMatch is not null)
            {
                GridMono.OnMatchCreated.Invoke(foundMatch);
            }
        }

        private Match CheckMatch()
        {
            _searchQueue.Clear();
            _searchedTiles.Clear();
            _matchedTiles.Clear();
            _searchQueue.Enqueue(CoreTile);

            _searchedTiles.Add(CoreTile);
            
            BoardElementType searchedType = CoreTile.GetFirstElement().Type;
            Tile currentTile = null;
            Tile neighbor = null;
            while (_searchQueue.Count > 0)
            {
                currentTile = _searchQueue.Dequeue();
                
                for (int i = 0; i < _searchDirections.Length; i++)
                {
                    neighbor = currentTile.GetNeighbor(_searchDirections[i]);
                    if(_searchedTiles.Contains(neighbor))
                        continue;
                    
                    _searchedTiles.Add(neighbor);

                    if (neighbor == null || neighbor.IsEmpty() ||
                        neighbor.GetFirstElement().Type != searchedType)
                    {
                        continue;
                    }

                    _matchedTiles.Add(neighbor);
                    _searchQueue.Enqueue(neighbor);
                }
            }

            if (_matchedTiles.Count <= 0) return null;
            
            _matchedTiles.Add(CoreTile);
            return new BasicMatch(MatchType.ThreeMatch, _matchedTiles);

        }
    }
}