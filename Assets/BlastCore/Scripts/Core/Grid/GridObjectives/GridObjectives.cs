using System.Collections.Generic;
using Match3.Grid.GridData;

namespace Match3.Grid
{
    public class GridObjectives
    {
        private Dictionary<BoardElementType, Objective> _objectives;
        private int _moveCount;
        
        public void InitializeObjectives(LevelData levelData)
        {
            _objectives = new Dictionary<BoardElementType, Objective>();
            foreach (ObjectiveData objectiveData in levelData.ObjectiveDataList)
            {
                _objectives.Add(objectiveData.ObjectiveType, new Objective(objectiveData.ObjectiveType, objectiveData.Count));
            }

            _moveCount = levelData.levelMoveCount;
        }

        public void ReduceObjective()
        {
            
        }

        public void ReduceMoveCount()
        {
            
        }
    }
}