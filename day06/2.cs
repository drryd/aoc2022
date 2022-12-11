using System.Collections;
using System.IO;
var input = File.ReadAllText("input.txt");

var r = 13;

List<char> chars = new();

for (var i = 0; i <= r; i++)
{
    chars.Add(input[i]);
}

while (r < input.Length)
{
    HashSet<char> set = new(chars);
    if (set.Count == 14)
    {
        Console.WriteLine(r + 1);
        return;
    }

    chars.RemoveAt(0);
    r += 1;
    chars.Add(input[r]);
}