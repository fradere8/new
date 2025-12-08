namespace Passwords;

public class CaseAlternatorTask
{
	public static List<string> AlternateCharCases(string lowercaseWord)
	{
		var result = new List<string>();
		AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
		return result;
	}

	static void AlternateCharCases(char[] word, int startIndex, List<string> result)
	{
		if (startIndex == word.Length)
		{
			result.Add(new string(word));
			return;
		}

		var charElement = word[startIndex];
		var variants = ChangeRegister(charElement, word, startIndex);

		foreach (var c in variants)
		{
			word[startIndex] = c;
			AlternateCharCases(word, startIndex + 1, result);
		}

		word[startIndex] = charElement;
	}

	public static char[] ChangeRegister(char charElement, char[] word, int startIndex)
    {
		char[] variants;

		if (char.IsLetter(charElement) && charElement != 223 && (charElement < 1425 || charElement > 1524))
		{
			variants = new[] { char.ToLower(charElement), char.ToUpper(charElement) };
		}
		else
		{
			variants = new[] { charElement }; 
		}

		return variants;
    }
}