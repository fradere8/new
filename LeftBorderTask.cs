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

		var m = (left + right) / 2;
		if (string.Compare(phrases[m], prefix, StringComparison.InvariantCultureIgnoreCase) < 0
					&& !phrases[m].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return GetLeftBorderIndex(phrases, prefix, m, right);
		return GetLeftBorderIndex(phrases, prefix, left, m);
	}
}

