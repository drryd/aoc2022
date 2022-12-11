// using System.Collections;
// using System.IO;
// var input = File.ReadAllText("input.txt");

// var r = 3;

// List<char> chars = new();

// chars.Add(input[0]);
// chars.Add(input[1]);
// chars.Add(input[2]);
// chars.Add(input[3]);

// while (r < input.Length)
// {
//     HashSet<char> set = new(chars);
//     if (set.Count == 4)
//     {
//         Console.WriteLine(r + 1);
//         return;
//     }

//     chars.RemoveAt(0);
//     r += 1;
//     chars.Add(input[r]);
// }