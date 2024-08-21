using Blast.Core.MatchLogic;

namespace Blast.Core.TileLogic
{
    public interface IMatchLocator
    {
        public void CheckAndActivateMatch(BoardElementType searchType, Tile startTile);
    }
}