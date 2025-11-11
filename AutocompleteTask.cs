using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Autocomplete;

internal class AutocompleteTask
{
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count 
				&& phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
            
		return null;
	}

	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
		var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
		var realCount = Math.Min(count, right - left);
		var res = new string[realCount];
		for (var i = 0; i < realCount; i++)
    {
			res[i] = phrases[left + i];
    }

		return res;
	}

	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
		var countPrefixPhrases = right - left;

		return countPrefixPhrases;
	}
}

[TestFixture]
public class AutocompleteTests
{
	[Test]
	public void TopByPrefix_IsEmpty_WhenNoPhrases()
	{
		var phrases = new string[0];
    	var prefix = "aabb";
		var count = 4;
		var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
		CollectionAssert.IsEmpty(actualTopWords);
	}

	[Test]
	public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
	{
		var phrases = new[] {"abcd", "adds", "rouj"};
		var prefix = "";
		var expectCount = phrases.Length;    
		var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Length) + 1;
		var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Length);
		var realCount = right - left;
		ClassicAssert.AreEqual(expectCount, realCount);
	}
}
