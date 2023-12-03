using Match3.Grid.GridData;
using Match3.Services.AssetManagement;
using Match3.Tile;
using UnityEngine;

namespace Match3.Grid.GridCreation
{
    public class TileElementCreator
    {
        private RandomStoneTypeAssigner _typeAssigner = new RandomStoneTypeAssigner();

        public BaseTileElement CreateTileElement(TileElementData tileElementData, GridMono grid, Vector3 spawnPosition)
        {
            if (tileElementData.ElementType == BoardElementType.RandomStone)
                tileElementData.ElementType = _typeAssigner.AssignRandomTypes(tileElementData, grid);
            
            return CreateElementFromPool(tileElementData, spawnPosition, grid);
        }

        public void CreateElementOnTile(TileElementData tileElementData, Tile.Tile createdTile, GridMono grid)
        {
            if (tileElementData.ElementType == BoardElementType.RandomStone)
                tileElementData.ElementType = _typeAssigner.AssignRandomTypes(tileElementData, grid);
            
            BaseTileElement baseTileElement = CreateElementFromPool(tileElementData, createdTile.WorldPosition, grid);

            createdTile.AddTileElement(baseTileElement);
        }


        private BaseTileElement CreateElementFromPool(TileElementData tileElementData, Vector3 spawnPosition, GridMono grid)
        {
            BaseTileElement baseTileElement = tileElementData.GenerateBase();

            GameObject tileElementGameObject = AssetManager.Prefabs.GetPrefab(tileElementData.ElementType);
            tileElementGameObject.name = $"TileElement {baseTileElement.Layer} / {tileElementData.ElementType}]";
            tileElementGameObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            tileElementGameObject.transform.SetParent(grid.ElementLayerTransforms[baseTileElement.Layer]);
            tileElementGameObject.SetActive(true);
            
            baseTileElement.InitBaseTileElement(tileElementData, tileElementGameObject.transform);
            return baseTileElement;
        }
    }
}