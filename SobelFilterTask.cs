using System;
using System.Drawing;
using Avalonia.Controls.Platform;
using Tmds.DBus.Protocol;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        var radius = (sx.GetLength(0)) / 2;
        var sy = ToTransMatrix(sx);
        for (var x = radius; x < width - radius; x++)
            for (var y = radius; y < height - radius; y++)
            {
                var gx = MultiplyMatrix(g, x, y, sx, radius);
                var gy = MultiplyMatrix(g, x, y, sy, radius);
                result[x,y] = Math.Sqrt(gx * gx + gy * gy);
            }
        return result;
	}

    private static double MultiplyMatrix(double[,] g, int xCenter, int yCenter, double[,] sx, int radius)
    {
        var m = 0.0;
        for (var i = -radius; i <= radius; i++)
        {
            for (var j = -radius; j <= radius; j++)
            {
                m += g[xCenter + i,yCenter + j]*sx[i + radius,j + radius];
            }
        }
        return m;
	}

    private static double[,] ToTransMatrix(double[,] sx)
    {
        var widthSx = sx.GetLength(0);
        var heightSx = sx.GetLength(1);
        var sy = new double[widthSx,heightSx];
        for (var x = 0; x < heightSx; x++)
        {
            for (var y = 0; y < widthSx; y++)
            {
                sy[x, y] = sx[y, x];
            }
        }
        return sy;
    }
}


