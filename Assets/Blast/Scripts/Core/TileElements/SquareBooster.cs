using System;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;

namespace Blast.Core.TileElements
{
    public sealed class SquareBooster : BaseTileElement, IClickActivatable {
        private readonly IMatchFactory _matchFactory;

        public SquareBooster(IMatchFactory matchFactory) {
            _matchFactory = matchFactory;
        }
        
        public void Activate(Match activatedMatch, Action onActivationComplete)
        {
        }

        public override TileLayerType Layer { get; protected set; } = TileLayerType.ItemLayer;
    }
}