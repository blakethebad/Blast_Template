using System;
using Blast.Core.MatchLogic;

namespace Blast.Core.TileElements.Interfaces
{
    public interface IActivatable
    {
        public void Activate(Match activatedMatch, Action onActivationComplete);
    }
}