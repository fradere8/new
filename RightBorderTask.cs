using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;

public class RightBorderTask
{
	public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
	{
		while (left + 1 < right)
		{
			var middle = (right + left) / 2;
			if (string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase) > 0
							&& !phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			{
				right = middle;
			}
			else
			{
				left = middle;
			}
		}
		
		return right;
	}
}
