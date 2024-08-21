using System.Collections.Generic;
using UnityEngine;

namespace Blast.Core.Grid.GridData
{ 
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public string LevelId;

        public int sizeY;
        public int sizeX;

        public int levelMoveCount;

        public int levelIndex;
        public List<TileData> TileDataList;

        public List<BoardElementType> ObjectiveDataList;
        public List<int> ObjectiveCountList;

        //Index number is maximum objective data and can be hardcoded here

        public int GetMaxObjectiveCount() => 3;

        public void Test()
        {
            Debug.LogError("1wpqwewqeelwq");
        }
    }
}

