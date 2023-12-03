using Blast.Scripts.Core.Grid.GridData;
using UnityEngine;

namespace Blast.Scripts.Core.Grid.GridCreation
{
    public class GridCreator
    {
        public void CreateGrid(GridMono grid, LevelData currentLevelData, ref Tile.Tile[][] tiles)
        {
            //Loop over tileDataList to create each tile
            for (int i = 0; i < currentLevelData.TileDataList.Count; i++)
            {
                CreateTile(currentLevelData.TileDataList[i], ref tiles);
            }
            
            //Loop over created tiles and find their neighbors
            for (int x = 0; x < grid.Size.x; x++)
                for (int y = 0; y < grid.Size.y; y++) 
                    grid.GetTile(x,y).FindNeighbors(grid);
        }
        
        private void CreateTile(TileData tileData, ref Tile.Tile[][] tiles)
        {
            Vector2Int tileCoordinates = new Vector2Int(tileData.xPos, tileData.yPos);
            Tile.Tile createdTile = new Tile.Tile(tileCoordinates);
            tiles[tileData.xPos][tileData.yPos] = createdTile;

            //For each of the tile element data we will create a tile element under created tile
            for (int i = 0; i < tileData.TileElementDataList.Count; i++)
            {
                tileData.TileElementDataList[i].TileData = tileData;
                GridMono.OnElementRequested.Invoke(tileData.TileElementDataList[i], createdTile);
            }
        }

    }
}