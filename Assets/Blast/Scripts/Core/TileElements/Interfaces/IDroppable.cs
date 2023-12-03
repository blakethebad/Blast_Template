using System;
using Blast.Core.TileLogic;

namespace Blast.Core.TileElements.Interfaces
{
    public interface IDroppable
    {
        public void Drop(Tile droppedTile, Action onDropComplete);
    }
}