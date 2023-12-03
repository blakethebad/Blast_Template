using System;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.Match;

namespace Blast.Scripts.Core.TileElements
{
    public class HorizontalBooster : Booster
    {
        public override void Activate(Match.Match activatedMatch, Action onActivationComplete)
        {
            Match.Match boosterMatch = new HorizontalBoosterMatch(MatchType.HorizontalBoosterMatch, Tile);
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();

            GridMono.OnMatchCreated.Invoke(boosterMatch);
            onActivationComplete.Invoke();
        }

        public override void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
        }

        public override void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            Activate(null, onBoosterActivated);
        }
    }
}