using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Grid.Match
{
    public class BasicMatch : Match
    {
        public BasicMatch(MatchType matchType, HashSet<Tile.Tile> matchedTiles) : base(matchType)
        {
            MatchedTiles.UnionWith(matchedTiles);
        }

        public override IEnumerator ExecuteMatch()
        {
            ActivateTileGroup(MatchedTiles);
            yield return null;
        }
    }
}