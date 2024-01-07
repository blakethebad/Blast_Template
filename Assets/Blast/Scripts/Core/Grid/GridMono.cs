using System;
using System.Collections.Generic;
using Blast.Core.Grid.GridCreation;
using Blast.Core.Grid.GridData;
using Blast.Core.MatchLogic;
using Blast.Core.TileElements;
using Blast.Core.TileLogic;
using Blast.Services.AssetManagement;
using Blast.Services.AssetManagement.AssetGroup;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blast.Core.Grid
{
    public class GridMono : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private int _currentLevelIndex;
        [SerializeField] private List<BaseAssetGroup> _assetGroups;

        private readonly List<Match> _activeMatches = new List<Match>();

        private readonly GridCreator _gridCreator = new();
        private readonly GridStateController _gridStateController = new ();
        private readonly GridObjectives _gridObjectives = new ();
        private readonly TileElementCreator _tileElementCreator = new ();

        private Tile[][] _tiles;
        public Vector2Int Size { get; private set; }
        public Dictionary<TileLayerType, Transform> ElementLayerTransforms { get; private set; }

        public static Action<TileElementData, Tile> OnElementRequested { get; private set; } 
        public static Action<Match> OnMatchCreated { get; private set; }
        public static Action<Match> OnMatchCompleted { get; private set; }

        private void Start()
        {
            ElementLayerTransforms = new Dictionary<TileLayerType, Transform>()
            {
                { TileLayerType.ItemLayer , new GameObject("Item Layer").transform},
                { TileLayerType.BlockerLayer , new GameObject("Blocker Layer").transform},
                { TileLayerType.TileAbilityLayer , new GameObject("Tile Ability Layer").transform}
            };
            OnElementRequested = CreateElementOnTile;
            OnMatchCreated = ActivateMatch;
            OnMatchCompleted = RemoveMatch;
            
            InitializeGrid();

            
        }

        private void InitializeGrid()
        {
            AssetManager.InitAssetManagement(_assetGroups);
            LevelData currentLevel = AssetManager.Levels.GetLevel(_currentLevelIndex);

            Size = new Vector2Int(currentLevel.sizeX, currentLevel.sizeY);
            AdjustCamera(Size.x, Size.y);

            _tiles = new Tile[Size.x][];
            for (int i = 0; i < Size.x; i++)
            {
                _tiles[i] = new Tile[Size.y];
            }
            
            _gridObjectives.InitializeObjectives(currentLevel);
            _gridCreator.CreateGrid(this, currentLevel, ref _tiles);

            _gridStateController.StartStateController(currentLevel, this);
            CoroutineTracker.InitializeCoroutineTracker(this);
        }
        
        private void AdjustCamera(int sizeX, int sizeY)
        {
            Camera.main.transform.position = new Vector3(0.5f * (sizeX - 1), (0.5f * (sizeY - 2)), -17f);
        }

        #region PublicMethods

        public void ReduceObjective() => _gridObjectives.ReduceObjective();
        public void ReduceMoveCount() => _gridObjectives.ReduceMoveCount();
        
        public Tile GetTile(int x, int y) => x >= Size.x || x < 0 || y < 0 || y >= Size.y ? null : _tiles[x][y] ?? null;

        public void CreateElementOnTile(TileElementData tileElementData, Tile tile) =>
            _tileElementCreator.CreateElementOnTile(tileElementData, tile, this);

        #endregion

        #region InputOverGrid
        
        public void OnPointerDown(PointerEventData eventData) => _gridStateController.OnEnter(eventData);
        public void OnDrag(PointerEventData eventData) => _gridStateController.OnDrag(eventData);
        public void OnPointerUp(PointerEventData eventData) => _gridStateController.OnUp(eventData);
        
        #endregion

        #region Grid Actions

                
        public void SendInputToPosition(Vector2Int position, Direction direction)
        {
            Tile clickedTile = GetTile(position.x, position.y);
            
            if(clickedTile is null)
                return;

            clickedTile.RecieveInput(direction);
        }

        private void ActivateMatch(Match match)
        {
            _activeMatches.Add(match);
            StartCoroutine(match.ExecuteMatch());
        }

        private void RemoveMatch(Match match)
        {
            _activeMatches.Remove(match);
        }
        #endregion
    }
}