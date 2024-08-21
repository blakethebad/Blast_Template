using System;
using System.Collections.Generic;
using Blast.Core.Grid.GridData;
using Blast.Core.TileElements;
using Blast.Core.TileLogic;
using Blast.Services.AssetManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Blast.Core.Grid.Factories
{
    public class TileElementFactory : ITileElementFactory {
        private IMatchFactory _matchFactory;
        private readonly Transform _entityTransform;
        public TileElementFactory(IMatchFactory matchFactory, Transform entityParent) {
            _entityTransform = entityParent;
            _matchFactory = matchFactory;
        }
        
        
        private List<BoardElementType> _randomStoneTypes = new List<BoardElementType>()
        {
            BoardElementType.RedStone, BoardElementType.GreenStone, BoardElementType.YellowStone,
            BoardElementType.PurpleStone, BoardElementType.BlueStone
        };

        void ITileElementFactory.CreateElementOnTile(TileElementData elementData, Tile createdTile)
        {
            if (elementData.ElementType == BoardElementType.RandomStone)
                elementData.ElementType = _randomStoneTypes[Random.Range(0, _randomStoneTypes.Count)];

            BaseTileElement baseTileElement = CreateElementFromPool(elementData, createdTile.WorldPosition);

            createdTile.AddTileElement(baseTileElement);
        }
        
        private BaseTileElement CreateElementFromPool(TileElementData tileElementData, Vector3 spawnPosition)
        {
            BaseTileElement baseTileElement = GenerateElementByType(tileElementData.ElementType);

            GameObject tileElementGameObject = AssetManager.Prefabs.GetPrefab(tileElementData.ElementType);
            tileElementGameObject.name = $"TileElement {baseTileElement.Layer} / {tileElementData.ElementType}]";
            tileElementGameObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            tileElementGameObject.transform.SetParent(_entityTransform);
            tileElementGameObject.SetActive(true);
            
            baseTileElement.InitBaseTileElement(tileElementData, tileElementGameObject.transform);
            return baseTileElement;
        }

        public BaseTileElement GenerateElementByType(BoardElementType elementType) {
            
            BaseTileElement tileElement = elementType switch {
                BoardElementType.RedStone => new ColorStone(),
                BoardElementType.GreenStone => new ColorStone(),
                BoardElementType.YellowStone => new ColorStone(),
                BoardElementType.PurpleStone => new ColorStone(),
                BoardElementType.BlueStone => new ColorStone(),
                
                BoardElementType.HorizontalBooster => new HorizontalBooster(_matchFactory),
                BoardElementType.VerticalBooster => new VerticalBooster(_matchFactory),
                BoardElementType.PlusBooster => new PlusBooster(_matchFactory),
                BoardElementType.SquareBooster => new SquareBooster(_matchFactory),
                BoardElementType.VortexBooster => new VortexBooster(_matchFactory),
                
                BoardElementType.StoneSpawner => new Spawner(this),
                
                _ => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null)
            };

            return tileElement;
        }
    }
}