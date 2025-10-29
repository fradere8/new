using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Media.TextFormatting.Unicode;

namespace Recognizer;

internal static class MedianFilterTask
{
	/* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
	public static double[,] MedianFilter(double[,] original)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		var res = new double[width,height];
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				res[x, y] = FindHood(x, y, original);
			}
		}
		return res;
	}
	private static double FindHood(int x, int y, double[,] original)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		var neighbors = new List<double>();
		for (int i = x - 1; i < x + 1; i++)
		{
			for (int j = y - 1; j < y + 1; j++)
			{
				if (i >= 0 && i <= width && j >= 0 && j <= height) neighbors.Add(original[i, j]);
			}
		}
		neighbors.Sort();
		int count = neighbors.Count;
		if (count % 2 == 1)
		{
			return neighbors[count / 2];
		}
		else
		{
			return (neighbors[count / 2 - 1] + neighbors[count / 2]) / 2.0;
		}
    }
}
