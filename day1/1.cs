using System;
using System.IO;

string[] lines = File.ReadAllLines("input.txt");

var maxElfCalorieCount = 0;

var currElfCalorieCount = 0;
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        currElfCalorieCount = 0;
        continue;
    }
    
    currElfCalorieCount += Int32.Parse(line);
    maxElfCalorieCount = Math.Max(maxElfCalorieCount, currElfCalorieCount);
}

Console.WriteLine(maxElfCalorieCount);