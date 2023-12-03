using System;
using Blast.Core.MatchLogic;

namespace Blast.Core.TileElements
{
    public class SquareBooster : Booster
    {
        public override void Activate(Match activatedMatch, Action onActivationComplete)
        {
            throw new NotImplementedException();
        }

        public override void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            throw new NotImplementedException();
        }

        public override void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated)
        {
            throw new NotImplementedException();
        }
    }
}