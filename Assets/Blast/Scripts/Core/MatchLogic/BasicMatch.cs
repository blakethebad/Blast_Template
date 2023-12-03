using System.Collections;
using System.Collections.Generic;
using Blast.Core.TileLogic;

namespace Blast.Core.MatchLogic
{
    public class BasicMatch : Match
    {
        public BasicMatch(MatchType matchType, HashSet<Tile> matchedTiles) : base(matchType)
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