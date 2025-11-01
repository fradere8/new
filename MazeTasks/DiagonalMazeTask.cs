namespace Mazes;

public static class DiagonalMazeTask
{
    private static void GoTo(Robot robot, int steps, Direction direction)
    {
        if (!robot.Finished) 
        {
            for (var i = 0; i < steps && !robot.Finished; i++)
            {
                robot.MoveTo(direction);
            }
        }
    }
    private static int ToStep(int a, int b)
    {
        return (a - 3)/(b - 2);
    }

    private static int Steping(int a, int b)
    {
        ;
    }

    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
        {
            if (width > height)
			{
                GoTo(robot, ToStep(width, height), Direction.Right);
                GoTo(robot, 1, Direction.Down);
            }
            else 
			{
                GoTo(robot, ToStep(height, width), Direction.Down);
                GoTo(robot, 1, Direction.Right);
            }
        }      
    }
}
