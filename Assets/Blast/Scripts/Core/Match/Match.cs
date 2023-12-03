using System.Collections;
using System.Collections.Generic;
using Blast.Scripts.Core.Grid;

namespace Blast.Scripts.Core.Match
{
    public abstract class Match
    {
        public MatchType MatchType { get; private set; }
        protected HashSet<Tile.Tile> MatchedTiles { get; private set;} = new HashSet<Tile.Tile>();
        protected HashSet<Tile.Tile> ActivatedTiles { get; private set; } = new HashSet<Tile.Tile>();
        private readonly List<Tile.Tile> _tempList = new List<Tile.Tile>();

        protected Match(MatchType matchType)
        {
            MatchType = matchType;
        }

        public abstract IEnumerator ExecuteMatch();
        
        protected void ActivateTileGroup(HashSet<Tile.Tile> tileGroup)
        {
            _tempList.Clear();
            _tempList.AddRange(tileGroup);
            foreach (Tile.Tile matchedTile in _tempList)
                ActivateTile(matchedTile);
        }

        protected void ActivateTile(Tile.Tile matchedTile)
        {
            if(matchedTile == null)
                return;
            
            matchedTile.Activate(this, OnActivationComplete);
            
            void OnActivationComplete()
            {
                MatchedTiles.Remove(matchedTile);
                ActivatedTiles.Add(matchedTile);
                if (MatchedTiles.Count <= 0)
                {
                    GridMono.OnMatchCompleted.Invoke(this);
                    foreach (Tile.Tile matchedTile in ActivatedTiles)
                    {
                        matchedTile.RefillTile();
                    }
                }
            }
        }
    }
}