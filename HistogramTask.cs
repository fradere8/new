using System;
using System.Globalization;
using System.Runtime.InteropServices;
namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var title = $"Рождаемость людей с именем '{name}'";
        var dates = CreateLineWithDates();
        
        var birthsCounts = CreateBirthCount(names, name);

        return new HistogramData(title, dates, birthsCounts);
    }

    private static string[] CreateLineWithDates()
    {
        var dates = new string[31];

        for (var d = 0; d < dates.Length; d++)
        {
            dates[d] = (d + 1).ToString();
        }

        return dates;
    }

    private static double[] CreateBirthCount(NameData[] names, string name)
    {
        var birthsCounts = new double[31];
        foreach (var personName in names)
        {
            if (personName.Name == name && personName.BirthDate.Day != 1)
            {
                birthsCounts[personName.BirthDate.Day - 1]++;
            }
        }

        return birthsCounts;
    }
}