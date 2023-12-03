namespace Match3.Tile
{
    //NOT IMPLEMENTED CURRENTY
    public class TileBackgroundLayer : ITileLayer
    {
        public BaseTileElement GetFirstElement()
        {
            throw new System.NotImplementedException();
        }

        public void AddTileElement(BaseTileElement tileElement)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveElement(BaseTileElement tileElement)
        {
            
        }

        public bool IsEmpty()
        {
            return true;
        }

        public bool LayerContainsType(BoardElementType type)
        {
            return false;
        }

        public BoardElementType GetFirstType()
        {
            return BoardElementType.None;
        }
    }
}