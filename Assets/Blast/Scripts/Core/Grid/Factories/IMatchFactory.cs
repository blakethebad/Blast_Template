using System.Collections.Generic;
using Blast.Core.MatchLogic;
using Blast.Core.TileLogic;

namespace Blast.Core.Grid.Factories
{
    public interface IMatchFactory {
        void ActivateMatch(Match match);
        void CreateAndActivateBasicMatch(HashSet<Tile> matchedTiles);
        void CreateAndActivatePlusBoosterMatch(Tile startTile);
        void CreateAndActivateVerticalBoosterMatch(Tile tile);
        void CreateAndActivateHorizontalBoosterMatch(Tile tile);
        void CreateAndActivateVortexBoosterMatch(Tile tile);
    }
}