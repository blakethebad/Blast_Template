using System.Collections.Generic;
using Blast.Core.MatchLogic;
using Blast.Core.TileLogic;

namespace Blast.Core.Grid.Factories
{
    public class MatchFactory : IMatchFactory
    {
        public void ActivateMatch(Match match) {
            match.ExecuteMatch();
        }

        public void CreateAndActivateBasicMatch(HashSet<Tile> matchedTiles) {
            Match basicMatch = new BasicMatch(MatchType.ThreeMatch, matchedTiles);
            ActivateMatch(basicMatch);
        }

        public void CreateAndActivatePlusBoosterMatch(Tile startTile) {
            
        }

        public void CreateAndActivateVerticalBoosterMatch(Tile tile) {
            
        }

        public void CreateAndActivateHorizontalBoosterMatch(Tile tile) {
            
        }

        public void CreateAndActivateVortexBoosterMatch(Tile tile) {
            
        }
    }
}