using System;
using System.IO;

string[] lines = File.ReadAllLines("input.txt");

var itemPriorityDictionary = buildItemPriorityDictionary();
var sumOfPriorities = 0;

for (var i = 0; i < lines.Length; i += 3)
{
    var badge = lines[i].Intersect(lines[i+1]).Intersect(lines[i+2]).ToArray()[0];

    sumOfPriorities += itemPriorityDictionary[badge];
}

Console.WriteLine(sumOfPriorities);

IDictionary<char, int> buildItemPriorityDictionary()
{
    IDictionary<char, int> itemPriorityDictionary = new Dictionary<char, int>();

    var ord = (int)'a';
    while (ord <= (int)'z')
    {
        itemPriorityDictionary.Add((char)ord, ord - (int)'a' + 1);

        ord += 1;
    }

    ord = (int)'A';
    while (ord <= (int)'Z')
    {
        itemPriorityDictionary.Add((char)ord, ord - (int)'A' + 27);

        ord += 1;
    }

    return itemPriorityDictionary;
}