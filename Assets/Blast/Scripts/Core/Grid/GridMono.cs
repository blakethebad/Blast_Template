using System.Collections;
using System.Collections.Generic;
using Blast.Core.Grid.Factories;
using Blast.Core.Grid.GridData;
using Blast.Core.TileLogic;
using Blast.Core.TileLogic.TileStates;
using Blast.Services.AssetManagement;
using Blast.Services.AssetManagement.AssetGroup;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast.Core.Grid
{
    public class GridMono : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    { 
        [SerializeField] private int currentLevelIndex; 
        [SerializeField] private List<BaseAssetGroup> assetGroups; 
        [SerializeField] private Transform entityParent;

        private Tile[][] Tiles => _tileFactory.GetTiles();
        private ITileFactory _tileFactory;
        private IMatchFactory _matchFactory;
        private IMatchLocator _matchLocator;
        private ITileElementFactory _tileElementFactory;
        private IInputCollector _inputCollector;
        private IRefillHandler _refillHandler;
        
        public Vector2Int Size { get; private set; }
        
        private void Start()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            AssetManager.InitAssetManagement(assetGroups);
            LevelData currentLevel = AssetManager.Levels.GetLevel(currentLevelIndex);

            entityParent = new GameObject("EntityTransform").transform;
            Size = new Vector2Int(currentLevel.sizeX, currentLevel.sizeY);
            
            Camera.main.transform.position = new Vector3(0.5f * (Size.x - 1), (0.5f * (Size.y - 2)), -17f);

            _matchFactory = new MatchFactory();
            _matchLocator = new MatchLocator(_matchFactory);
            _tileElementFactory = new TileElementFactory(_matchFactory, entityParent);
            _refillHandler = new RefillHandler(this);
            _tileFactory = new TileFactory(_tileElementFactory, _matchLocator, _refillHandler);
            _inputCollector = new GridInputCollector(SendInputToPosition, Camera.main);

            _tileFactory.GenerateTiles(currentLevel);

            for (int i = 0; i < Size.x; i++) {
                for (int j = 0; j < Size.y; j++) {
                    Tiles[i][j].FindNeighbors(this);
                }
            }

            _refillHandler.StartGridRefill();
        }

        public Tile GetTile(int x, int y) => x >= Size.x || x < 0 || y < 0 || y >= Size.y ? null : Tiles[x][y] ?? null;
        public void OnPointerDown(PointerEventData eventData) => _inputCollector.OnGridClick(eventData.position);
        public void OnPointerUp(PointerEventData eventData) => _inputCollector.OnPointerUp(eventData.position);
        
        public void SendInputToPosition(Vector2Int position)
        {
            Tile clickedTile = GetTile(position.x, position.y);
            
            if(clickedTile is null)
                return;

            clickedTile.RecieveInput();
        }
    }
}