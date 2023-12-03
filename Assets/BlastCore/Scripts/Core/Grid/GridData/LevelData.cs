using System.Collections.Generic;
using UnityEngine;

namespace Match3.Grid.GridData
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
        
        //Index number is maximum objective data and can be hardcoded here
        public List<ObjectiveData> ObjectiveDataList = new List<ObjectiveData>();

        public int GetMaxObjectiveCount() => 3;

        public void Test()
        {
            Debug.LogError("1wpqwewqeelwq");
        }
    }
}

