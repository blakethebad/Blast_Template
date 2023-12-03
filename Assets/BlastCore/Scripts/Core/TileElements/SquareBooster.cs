using System;
using Match3.Grid.Match;

namespace Match3.Tile
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