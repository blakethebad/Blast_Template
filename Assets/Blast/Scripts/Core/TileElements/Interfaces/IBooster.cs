using System;

namespace Blast.Core.TileElements.Interfaces
{
    public interface IBooster 
    {
        public void ComboActivate(BaseTileElement swappedElement, Action onBoosterActivated);
        public void SwapActivate(BaseTileElement swappedElement, Action onBoosterActivated);
    }
}