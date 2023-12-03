using System;
using Match3.Grid.Match;

namespace Match3.Tile.Interfaces
{
    public interface IBooster 
    {
        public void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated);
        public void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated);
    }
}