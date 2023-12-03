using System;

namespace Blast.Scripts.Core.TileElements
{
    public class SquareBooster : Booster
    {
        public override void Activate(Match.Match activatedMatch, Action onActivationComplete)
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