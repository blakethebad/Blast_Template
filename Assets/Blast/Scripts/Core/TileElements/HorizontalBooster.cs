using System;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;

namespace Blast.Core.TileElements
{
    public sealed class HorizontalBooster : BaseTileElement, IClickActivatable {
        private readonly IMatchFactory _matchFactory;

        public HorizontalBooster(IMatchFactory matchFactory) {
            _matchFactory = matchFactory;
        }
        
        void IActivatable.Activate(Match activatedMatch, Action onActivationComplete)
        {
            Match boosterMatch = new HorizontalBoosterMatch(MatchType.HorizontalBoosterMatch, Tile);
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();
            _matchFactory.CreateAndActivateHorizontalBoosterMatch(Tile);
            //GridMono.OnMatchCreated.Invoke(boosterMatch);
            onActivationComplete.Invoke();
        }

        public override TileLayerType Layer { get; protected set; } = TileLayerType.ItemLayer;
    }
}