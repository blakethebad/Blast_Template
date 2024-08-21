using System;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;

namespace Blast.Core.TileLogic
{
    public class MatchLocator : IMatchLocator {
        private IMatchFactory _matchFactory;
        public MatchLocator(IMatchFactory matchFactory) {
            _matchFactory = matchFactory;
        }
        
        private readonly Queue<Tile> _searchQueue = new Queue<Tile>();
        private readonly HashSet<Tile> _searchedTiles = new HashSet<Tile>();
        private readonly HashSet<Tile> _matchedTiles = new HashSet<Tile>();
        private readonly Direction[] _searchDirections = new [] { Direction.Top , Direction.Bottom,
            Direction.Right, Direction.Left};

        void IMatchLocator.CheckAndActivateMatch(BoardElementType searchType, Tile startTile)
        {
            _searchQueue.Clear();
            _searchedTiles.Clear();
            _matchedTiles.Clear();
            _searchQueue.Enqueue(startTile);

            _searchedTiles.Add(startTile);
            
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
                        neighbor.GetFirstElement().Type != searchType)
                    {
                        continue;
                    }

                    _matchedTiles.Add(neighbor);
                    _searchQueue.Enqueue(neighbor);
                }
            }

            if (_matchedTiles.Count <= 0) return;
            
            _matchedTiles.Add(startTile);
            _matchFactory.CreateAndActivateBasicMatch(_matchedTiles);
        }
    }
}