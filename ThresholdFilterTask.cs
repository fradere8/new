using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		var count = width * height;
		var whitePx = new List<double>();
		var countwp = (int)(count * whitePixelsFraction);
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				whitePx.Add(original[x, y]);
			}
		}
		whitePx.Sort();
		whitePx.Reverse();
		whitePx.RemoveRange(countwp + 1, count - countwp - 1);
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				if (whitePx.Contains(original[x, y]))
				{
					original[x, y] = 1.0;
				}
				else
                {
                    original[x, y] = 0.0;
                }
			}
		}

		return original;
	}
}
