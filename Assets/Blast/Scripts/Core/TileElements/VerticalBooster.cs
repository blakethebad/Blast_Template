﻿using System;
using Blast.Core.Grid;
using Blast.Core.Grid.Factories;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements.Interfaces;

namespace Blast.Core.TileElements
{
    public sealed class VerticalBooster : BaseTileElement, IClickActivatable {
        private readonly IMatchFactory _matchFactory;

        public VerticalBooster(IMatchFactory matchFactory) {
            _matchFactory = matchFactory;
        }
        
        public void Activate(Match activatedMatch, Action onActivationComplete)
        {
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();
            _matchFactory.CreateAndActivateVerticalBoosterMatch(Tile);
            onActivationComplete.Invoke();
            
        }

        public override TileLayerType Layer { get; protected set; } = TileLayerType.ItemLayer;
    }
}