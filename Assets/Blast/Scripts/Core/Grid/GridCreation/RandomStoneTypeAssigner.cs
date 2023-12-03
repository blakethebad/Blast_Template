using System.Collections.Generic;
using Blast.Core.Grid.GridData;

namespace Blast.Core.Grid.GridCreation
{
    public class RandomStoneTypeAssigner
    {
        private List<BoardElementType> _stoneTypes;
        public BoardElementType AssignRandomTypes(TileElementData tileElementData, GridMono grid)
        {
            _stoneTypes = new List<BoardElementType>()
            {
                BoardElementType.BlueStone,
                BoardElementType.GreenStone,
                BoardElementType.YellowStone,
                BoardElementType.RedStone,
                BoardElementType.PurpleStone,
            };
            
            CheckMatchPossiblitiesAndRemoveTypes(tileElementData.TileData.xPos, tileElementData.TileData.yPos, grid);

            return _stoneTypes[UnityEngine.Random.Range(0, _stoneTypes.Count)];
        }

        private void CheckMatchPossiblitiesAndRemoveTypes(int x, int y, GridMono grid)
        {
            BoardElementType leftType = BoardElementType.RandomStone;
            if (x > 0 && grid.GetTile(x - 1, y) != null)
                leftType = grid.GetTile(x - 1, y).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType farLeftType = BoardElementType.RandomStone;
            if (x > 1 && grid.GetTile(x - 2, y) != null)
                farLeftType = grid.GetTile(x - 2, y).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType rightType = BoardElementType.RandomStone;
            if (x < grid.Size.x - 1 && grid.GetTile(x + 1, y) != null)
                rightType = grid.GetTile(x + 1, y).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType farRightType = BoardElementType.RandomStone;
            if (x < (grid.Size.x - 2) && (grid.GetTile(x + 2, y) != null))
                farRightType = grid.GetTile(x + 2, y).GetFirstTypeFromLayer(TileLayerType.ItemLayer);
            
            BoardElementType bottomType = BoardElementType.RandomStone;
            if (y > 0 && grid.GetTile(x, y - 1) != null)
                bottomType = grid.GetTile(x, y - 1).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType farBottomType = BoardElementType.RandomStone;
            if (y > 1 && grid.GetTile(x, y - 2) != null)
                farBottomType = grid.GetTile(x, y - 2).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType topType = BoardElementType.RandomStone;
            if (y < grid.Size.y - 1 && grid.GetTile(x, y + 1) != null)
                topType = grid.GetTile(x, y + 1).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType farTopType = BoardElementType.RandomStone;
            if (y < grid.Size.y - 2 && grid.GetTile(x, y + 2) != null)
                farTopType = grid.GetTile(x, y + 2).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

            BoardElementType diagonalType = BoardElementType.RandomStone;
            
            if (leftType != BoardElementType.RandomStone && (farLeftType == leftType || leftType == rightType))
                _stoneTypes.Remove(leftType);
            
            if (rightType != BoardElementType.RandomStone && (rightType == farRightType))
                _stoneTypes.Remove(rightType);
            
            if (bottomType != BoardElementType.RandomStone && (farBottomType == bottomType || bottomType == topType))
                _stoneTypes.Remove(bottomType);

            if (topType != BoardElementType.RandomStone && topType == farTopType)
                _stoneTypes.Remove(topType);

            if (leftType != BoardElementType.RandomStone)
            {
                if (bottomType == leftType && grid.GetTile(x - 1, y - 1) != null)
                    diagonalType = grid.GetTile(x - 1, y - 1).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

                if (topType == leftType && grid.GetTile(x - 1, y + 1) != null)
                    diagonalType = grid.GetTile(x - 1, y + 1).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

                if (diagonalType != BoardElementType.RandomStone)
                    _stoneTypes.Remove(diagonalType);
            }

            if (rightType != BoardElementType.RandomStone)
            {
                if (bottomType == rightType && grid.GetTile(x + 1, y - 1) != null)
                    diagonalType = grid.GetTile(x + 1, y - 1).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

                if (topType == rightType && grid.GetTile(x + 1, y + 1) != null) 
                    diagonalType = grid.GetTile(x + 1, y + 1).GetFirstTypeFromLayer(TileLayerType.ItemLayer);

                if (diagonalType != BoardElementType.RandomStone) 
                    _stoneTypes.Remove(diagonalType);
            }
        }

    }
}