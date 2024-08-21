using System.Numerics;
using Blast.Core.Grid.GridData;
using Blast.Core.TileLogic;
using Blast.Core.TileLogic.TileStates;
using UnityEngine;

namespace Blast.Core.Grid.Factories {
    public interface ITileFactory {
        void GenerateTiles(LevelData currentLevelData);
        Tile[][] GetTiles();
        void SwitchToIdle(Tile tile);
        void SwitchToEmpty(Tile tile);
    }
}