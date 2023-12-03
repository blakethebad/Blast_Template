using System;

namespace Blast.Scripts.Core.TileElements.Interfaces
{
    public interface IDroppable
    {
        public void Drop(Tile.Tile droppedTile, Action onDropComplete);
    }
}