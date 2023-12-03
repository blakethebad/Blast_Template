using System.Collections;
using System.Collections.Generic;

namespace Blast.Scripts.Core.Match
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