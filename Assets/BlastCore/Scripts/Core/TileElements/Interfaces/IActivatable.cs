using System;
using Match3.Grid;
using Match3.Grid.Match;

namespace Match3.Tile.Interfaces
{
    public interface IActivatable
    {
        public void Activate(Match activatedMatch, Action onActivationComplete);
    }
}