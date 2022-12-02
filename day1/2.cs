using System;
using System.IO;

string[] lines = File.ReadAllLines("input.txt");

List<int> elfCalorieCounts = new List<int>();

var currElfCalorieCount = 0;
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        elfCalorieCounts.Add(currElfCalorieCount);
        currElfCalorieCount = 0;

        continue;
    }

    currElfCalorieCount += Int32.Parse(line);
}

elfCalorieCounts.Sort();

Console.WriteLine(elfCalorieCounts.TakeLast(3).Sum());