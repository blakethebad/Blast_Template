using System.Collections;
using System.Collections.Generic;
using Blast.Core.Grid;
using Blast.Core.TileLogic;

namespace Blast.Core.MatchLogic
{
    public abstract class Match
    {
        public MatchType MatchType { get; private set; }
        protected HashSet<Tile> MatchedTiles { get; private set;} = new HashSet<Tile>();
        protected HashSet<Tile> ActivatedTiles { get; private set; } = new HashSet<Tile>();
        private readonly List<Tile> _tempList = new List<Tile>();

        protected Match(MatchType matchType)
        {
            MatchType = matchType;
        }

        public abstract void ExecuteMatch();

        protected void ActivateTileGroup(HashSet<Tile> tileGroup)
        {
            _tempList.Clear();
            _tempList.AddRange(tileGroup);
            foreach (Tile matchedTile in _tempList)
                ActivateTile(matchedTile);
        }

        private void ActivateTile(Tile matchedTile)
        {
            if(matchedTile == null)
                return;
            
            matchedTile.Activate(this, OnActivationComplete);
            
            void OnActivationComplete()
            {
                MatchedTiles.Remove(matchedTile);
                ActivatedTiles.Add(matchedTile);
                if (MatchedTiles.Count > 0) return;
            }
        }
    }
}