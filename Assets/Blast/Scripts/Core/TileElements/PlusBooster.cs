using System;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;

namespace Blast.Core.TileElements
{
    public class PlusBooster : BaseTileElement, IClickActivatable {
        private IMatchFactory _matchFactory;
        private Match _boosterMatch;

        public PlusBooster(IMatchFactory matchFactory) {
            _matchFactory = matchFactory;
        }
        
        public override TileLayerType Layer { get; protected set; }
        public void Activate(Match activatedMatch, Action onActivationComplete)
        {
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();
            _matchFactory.CreateAndActivatePlusBoosterMatch(Tile);
            _matchFactory.ActivateMatch(_boosterMatch);
            onActivationComplete.Invoke();
        }
    }
}