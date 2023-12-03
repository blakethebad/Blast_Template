using System;

namespace Match3.Tile.Interfaces
{
    public interface IDroppable
    {
        public void Drop(Tile droppedTile, Action onDropComplete);
    }
}