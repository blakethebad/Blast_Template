using System;
using Blast.Core.Grid;
using Blast.Core.MatchLogic;

namespace Blast.Core.TileElements
{
    public class PlusBooster : Booster
    {
        private Match _boosterMatch;
        public override void Activate(Match activatedMatch, Action onActivationComplete)
        {
            _boosterMatch = new PlusBoosterMatch(MatchType.PlusBoosterMatch, Tile);
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