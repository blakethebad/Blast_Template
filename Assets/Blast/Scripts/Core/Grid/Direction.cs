namespace Blast.Core.Grid
{
    public enum Direction
    {
        None,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        
    }

    public static class DirectionHelper
    {
        public static Direction Opposite(Direction direction)
        {
            return direction switch
            {
                Direction.TopLeft => Direction.BottomRight,
                Direction.Top => Direction.Bottom,
                Direction.TopRight => Direction.BottomRight,
                Direction.Right => Direction.Left,
                Direction.BottomRight => Direction.TopLeft,
                Direction.Bottom => Direction.Top,
                Direction.BottomLeft => Direction.TopRight,
                Direction.Left => Direction.Right,
                _ => Direction.None
            };
        }

        public static Direction ClockwiseDirection(Direction direction)
        {
            return direction switch
            {
                Direction.TopLeft => Direction.Top,
                Direction.Top => Direction.TopRight,
                Direction.TopRight => Direction.Right,
                Direction.Right => Direction.BottomRight,
                Direction.BottomRight => Direction.Bottom,
                Direction.Bottom => Direction.BottomLeft,
                Direction.BottomLeft => Direction.Left,
                Direction.Left => Direction.TopLeft,
                _ => Direction.None
            };
        }
    }
}