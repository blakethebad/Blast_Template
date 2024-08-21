using Blast.Core.Grid.GridData;
using Blast.Core.TileLogic;
using Blast.Core.TileLogic.TileStates;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Blast.Core.Grid.Factories {
    public class TileFactory : ITileFactory
    {
        private readonly ITileElementFactory _tileElementFactory;
        private readonly IMatchLocator _matchLocator;
        private readonly IRefillHandler _refillHandler;

        private Tile[][] _emptyTiles;
        private Tile[][] _idleTiles; 

        private Tile[][] _gameTiles;

        public TileFactory(ITileElementFactory tileElementFactory, IMatchLocator matchLocator, IRefillHandler refillHandler)
        {
            _tileElementFactory = tileElementFactory;
            _matchLocator = matchLocator;
            _refillHandler = refillHandler;
        }
        
        void ITileFactory.GenerateTiles(LevelData currentLevelData)
        {
            _gameTiles = new Tile[currentLevelData.sizeX][];
            for (int i = 0; i < currentLevelData.sizeX; i++)
            {
                _gameTiles[i] = new Tile[currentLevelData.sizeY];
            }
            
            _idleTiles = new Tile[currentLevelData.sizeX][];
            for (int i = 0; i < currentLevelData.sizeX; i++)
            {
                _idleTiles[i] = new Tile[currentLevelData.sizeY];
            }
            
            _emptyTiles = new Tile[currentLevelData.sizeX][];
            for (int i = 0; i < currentLevelData.sizeX; i++)
            {
                _emptyTiles[i] = new Tile[currentLevelData.sizeY];
            }

            foreach (var tileData in currentLevelData.TileDataList)
            {
                Vector2Int tileCoordinates = new Vector2Int(tileData.xPos, tileData.yPos);
                IdleTile idleTile = new IdleTile(tileCoordinates, _refillHandler, _matchLocator, this);
                EmptyTile emptyTile = new EmptyTile(tileCoordinates, _refillHandler, this);

                _idleTiles[tileData.xPos][tileData.yPos] = idleTile;
                _emptyTiles[tileData.xPos][tileData.yPos] = emptyTile;
                
                if (tileData.TileElementDataList.Count <= 0) {
                    _gameTiles[tileData.xPos][tileData.yPos] = emptyTile;
                }
                else {
                    _gameTiles[tileData.xPos][tileData.yPos] = idleTile;
                }

                //For each of the tile element data we will create a tile element under created tile
                for (int j = 0; j < tileData.TileElementDataList.Count; j++)
                {
                    tileData.TileElementDataList[j].TileData = tileData;
                    _tileElementFactory.CreateElementOnTile(tileData.TileElementDataList[j], _gameTiles[tileData.xPos][tileData.yPos]);
                }
            }
        }

        public Tile[][] GetTiles() => _gameTiles;

        public void SwitchToIdle(Tile tile) {
            Tile idleTile = _idleTiles[tile.Coordinates.x][tile.Coordinates.y];
            idleTile.SetNeighbors(tile.GetNeighbors());
            idleTile.SetLayers(tile.GetLayers());
            _gameTiles[tile.Coordinates.x][tile.Coordinates.y] = idleTile;
        }

        public void SwitchToEmpty(Tile tile) {
            Debug.LogError(tile.Coordinates);
            Tile emptyTile = _emptyTiles[tile.Coordinates.x][tile.Coordinates.y];
            emptyTile.SetNeighbors(tile.GetNeighbors());
            emptyTile.SetLayers(tile.GetLayers());
            _gameTiles[tile.Coordinates.x][tile.Coordinates.y] = emptyTile;
        }
    }
}