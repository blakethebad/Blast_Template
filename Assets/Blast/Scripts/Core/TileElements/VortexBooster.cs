using System;
using Blast.Scripts.Core.Grid;
using Blast.Scripts.Core.Match;

namespace Blast.Scripts.Core.TileElements
{
    public class VortexBooster : Booster
    {
        private Match.Match _boosterMatch;
        public override void Activate(Match.Match activatedMatch, Action onActivationComplete)
        {
            _boosterMatch = new HorizontalBoosterMatch(MatchType.HorizontalBoosterMatch, Tile);
            Tile.RemoveElementFromTile(this);
            ReturnElementToPool();
            GridMono.OnMatchCreated.Invoke(_boosterMatch);
            onActivationComplete.Invoke();
        }

        public override void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            throw new NotImplementedException();
        }

        public override void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            Activate(null, onBoosterActivated);
        }
    }
}