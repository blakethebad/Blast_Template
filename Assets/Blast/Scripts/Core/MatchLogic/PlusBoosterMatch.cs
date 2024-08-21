using System.Collections;
using Blast.Core.Grid;
using Blast.Core.TileLogic;

namespace Blast.Core.MatchLogic
{
    public class PlusBoosterMatch : Match
    {
        public PlusBoosterMatch(MatchType matchType, Tile originTile) : base(matchType)
        {
            CalculateMatch(originTile);
        }

        public override void ExecuteMatch()
        {
            ActivateTileGroup(MatchedTiles);
        }

        private void CalculateMatch(Tile originTile)
        {
            MatchedTiles.Add(originTile);
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Top));
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Bottom));
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Left));
            MatchedTiles.Add(originTile.GetNeighbor(Direction.Right));
        }
    }
}