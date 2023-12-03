using System;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.Match;

namespace Blast.Scripts.Core.TileElements
{
    public class VerticalBooster : Booster
    {
        private Match.Match _boosterMatch;
        public override void Activate(Match.Match activatedMatch, Action onActivationComplete)
        {
            _boosterMatch = new VerticalBoosterMatch(MatchType.VerticalBoosterMatch, Tile);
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();

            GridMono.OnMatchCreated.Invoke(_boosterMatch);
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