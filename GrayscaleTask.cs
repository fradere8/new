using System.Drawing;
using System.Runtime.CompilerServices;

namespace Recognizer;

public static class GrayscaleTask
{
	/* 
	 * Переведите изображение в серую гамму.
	 * 
	 * original[x, y] - массив пикселей с координатами x, y. 
	 * Каждый канал R,G,B лежит в диапазоне от 0 до 255.
	 * 
	 * Получившийся массив должен иметь те же размеры, 
	 * grayscale[x, y] - яркость пикселя (x,y) в диапазоне от 0.0 до 1.0
	 *
	 * Используйте формулу:
	 * Яркость = (0.299*R + 0.587*G + 0.114*B) / 255
	 * 
	 * Почему формула именно такая — читайте в википедии 
	 * http://ru.wikipedia.org/wiki/Оттенки_серого
	 */

	public static double[,] ToGrayscale(Pixel[,] original)
	{
		double[,] brightness = new double[original.GetLength(0), original.GetLength(1)];
		for (var i = 0; i < original.GetLength(0); i++)
		{
			for (var j = 0; j < original.GetLength(1); j++)
			{
				Pixel pixel = original[i, j];
				brightness[i, j] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
			}
		}
		return brightness;
	}
}
