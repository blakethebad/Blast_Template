using System;
using Blast.Core.TileElements;

namespace Blast.Core.Grid.GridData
{
    [Serializable]
    public class SpawnerData : TileElementData
    {
        public override BaseTileElement GenerateBase() => new Spawner();
    }
}