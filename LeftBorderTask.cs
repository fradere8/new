using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;
public class LeftBorderTask
{
	public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
	{
		if (left + 1 >= right) 
			return left;

		var m = left + (right - left) / 2;
		var isLessThanPrefix = string.Compare(phrases[m], prefix, StringComparison.InvariantCultureIgnoreCase) < 0;
		var startsWithPrefix = phrases[m].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);

		if (isLessThanPrefix && !startsWithPrefix)
			return GetLeftBorderIndex(phrases, prefix, m, right);

		return GetLeftBorderIndex(phrases, prefix, left, m);
	}
} 
 
