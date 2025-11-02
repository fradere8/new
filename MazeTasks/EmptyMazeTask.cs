namespace Mazes;

public static class EmptyMazeTask
{
	public static void MoveOut(Robot robot, int width, int height)
	{
		void MovingRight(int width)
		{
			for (int i = 0; i < width; i++)
			{
				robot.MoveTo(Direction.Right);
			}
		}

		void MovingDown(int height)
		{
			for (int i = 0; i < width; i++)
			{
				robot.MoveTo(Direction.Right);
			}
		}
	}
}
