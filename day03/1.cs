// using System;
// using System.IO;

// string[] lines = File.ReadAllLines("input.txt");

// var itemPriorityDictionary = buildItemPriorityDictionary();
// var sumOfPriorities = 0;

// foreach (var line in lines)
// {
//     var (c1, c2) = getCompartments(line);
//     var itemInBothCompartments = c1.Intersect(c2).ToArray()[0];

//     sumOfPriorities += itemPriorityDictionary[itemInBothCompartments];
// }

// Console.WriteLine(sumOfPriorities);

// (string, string) getCompartments(string rucksack)
// {
//     return (rucksack.Substring(0, rucksack.Length / 2), rucksack.Substring(rucksack.Length / 2, rucksack.Length / 2));
// }

// IDictionary<char, int> buildItemPriorityDictionary()
// {
//     IDictionary<char, int> itemPriorityDictionary = new Dictionary<char, int>();

//     var ord = (int)'a';
//     while (ord <= (int)'z')
//     {
//         itemPriorityDictionary.Add((char)ord, ord - (int)'a' + 1);

//         ord += 1;
//     }

//     ord = (int)'A';
//     while (ord <= (int)'Z')
//     {
//         itemPriorityDictionary.Add((char)ord, ord - (int)'A' + 27);

//         ord += 1;
//     }

//     return itemPriorityDictionary;
// }