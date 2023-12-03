using System.Collections;
using Blast.Scripts.Core.Grid;

namespace Blast.Scripts.Core.Match
{
    public class PlusBoosterMatch : Match
    {
        public PlusBoosterMatch(MatchType matchType, Tile.Tile originTile) : base(matchType)
        {
            CalculateMatch(originTile);
        }

        public override IEnumerator ExecuteMatch()
        {
            ActivateTileGroup(MatchedTiles);
            yield break;
        }

        private void CalculateMatch(Tile.Tile originTile)
        {
            MatchedTiles.Add(originTile);
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Top));
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Bottom));
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Left));
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Right));
        }
    }
}