namespace Blast.Scripts.Core.Grid.GridObjectives
{
    public class Objective
    {
        public BoardElementType ObjectiveType { get; set; }
        public int ObjectiveCount { get; set; }

        public Objective(BoardElementType type, int objectiveCount)
        {
            ObjectiveType = type;
            ObjectiveCount = objectiveCount;
        }
    }
}