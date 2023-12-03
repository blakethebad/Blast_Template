using Blast.Core.Grid.GridData;
using Blast.Core.TileElements;
using Blast.Core.TileLogic;
using Blast.Services.AssetManagement;
using UnityEngine;

namespace Blast.Core.Grid.GridCreation
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

        public void CreateElementOnTile(TileElementData tileElementData, Tile createdTile, GridMono grid)
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