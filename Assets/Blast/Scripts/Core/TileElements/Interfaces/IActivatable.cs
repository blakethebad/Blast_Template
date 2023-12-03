using System;

namespace Blast.Scripts.Core.TileElements.Interfaces
{
    public interface IActivatable
    {
        public void Activate(Match.Match activatedMatch, Action onActivationComplete);
    }
}