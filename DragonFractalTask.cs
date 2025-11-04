using System;
using Avalonia;
using Avalonia.Controls.Platform;

namespace Fractals;

internal static class DragonFractalTask
{
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
		double x = 1;
        double y = 0;

        var count = new Random(seed);
        for (var i = 0; i <= iterationsCount; i++) {
            var operation = count.Next(2);
            double x1, y1;
            if (operation == 0)
            {
                x1 = TransformCordinate(x, y, 0, 45)[0];
                y1 = TransformCordinate(x, y, 0, 45)[1];
            }
            else
            {
                x1 = TransformCordinate(x, y, 1, 135)[0];
                y1 = TransformCordinate(x, y, 0, 135)[1];
            }
            pixels.SetPixel(x, y);
            x = x1;
            y = y1;
        }
    }

    private static double[] TransformCordinate(double x, double y, int z, int angle) {
		var cordinates = new double[2];
		var cos = Math.Cos(angle * Math.PI / 180);
		var sin = Math.Sin(angle * Math.PI / 180);
        cordinates[0] = (x * cos - y * sin) / Math.Sqrt(2) + z;
        cordinates[1] = (x * sin + y * cos) / Math.Sqrt(2);

        return cordinates; 
    }
}
