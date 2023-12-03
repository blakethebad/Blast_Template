using System;
using Match3.Tile;

namespace Match3.Grid.GridData
{
    [Serializable]
    public class SpawnerData : TileElementData
    {
        public override BaseTileElement GenerateBase() => new Spawner();
    }
}