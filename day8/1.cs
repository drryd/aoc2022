// var lines = System.IO.File.ReadAllLines("input.txt");
// var SIDE_SIZE = lines.Length; // Assuming all sides are equal.

// var map = parseMapFromLines(lines);
// var visible = new int[SIDE_SIZE, SIDE_SIZE];

// scanAndMarkAsVisible(map, visible);
// Console.WriteLine(visible.Cast<int>().Sum());

// void markEdgesAsVisible(int[,] visible)
// {
//     // top row / bottom row
//     for (var c = 0; c < SIDE_SIZE; c++)
//     {
//         visible[0, c] = 1;
//         visible[SIDE_SIZE - 1, c] = 1;
//     }

//     // left column / right column
//     for (var r = 0; r < SIDE_SIZE; r++)
//     {
//         visible[r, 0] = 1;
//         visible[r, SIDE_SIZE - 1] = 1;
//     }
// }

// void scanAndMarkAsVisible(int[,] map, int[,] visible)
// {
//     markEdgesAsVisible(visible);
//     markVisibleFromLeft(map, visible);
//     markVisibleFromRight(map, visible);
//     markVisibleFromTop(map, visible);
//     markVisibleFromBottom(map, visible);
// }

// void markVisibleFromLeft(int[,] map, int[,] visible)
// {
//     for (var r = 1; r < SIDE_SIZE - 1; r++)
//     {
//         var highestSoFar = map[r, 0];
//         for (var c = 1; c < SIDE_SIZE - 1; c++)
//         {
//             if (map[r, c] > highestSoFar)
//             {
//                 visible[r, c] = 1;
//                 highestSoFar = map[r, c];
//             }
//         }
//     }
// }

// void markVisibleFromRight(int[,] map, int[,] visible)
// {
//     for (var r = 1; r < SIDE_SIZE - 1; r++)
//     {
//         var highestSoFar = map[r, SIDE_SIZE - 1];
//         for (var c = SIDE_SIZE - 2; c > 0; c--)
//         {
//             if (map[r, c] > highestSoFar)
//             {
//                 visible[r, c] = 1;
//                 highestSoFar = map[r, c];
//             }
//         }
//     }
// }

// void markVisibleFromTop(int[,] map, int[,] visible)
// {
//     for (var c = 1; c < SIDE_SIZE - 1; c++)
//     {
//         var highestSoFar = map[0, c];
//         for (var r = 1; r < SIDE_SIZE - 1; r++)
//         {
//             if (map[r, c] > highestSoFar)
//             {
//                 visible[r, c] = 1;
//                 highestSoFar = map[r, c];
//             }
//         }
//     }
// }

// void markVisibleFromBottom(int[,] map, int[,] visible)
// {
//     for (var c = 1; c < SIDE_SIZE - 1; c++)
//     {
//         var highestSoFar = map[SIDE_SIZE - 1, c];
//         for (var r = SIDE_SIZE - 2; r > 0; r--)
//         {
//             if (map[r, c] > highestSoFar)
//             {
//                 visible[r, c] = 1;
//                 highestSoFar = map[r, c];
//             }
//         }
//     }
// }

// int[,] parseMapFromLines(string[] lines)
// {
//     var map = new int[lines.Length, lines.Length];

//     for (var r = 0; r < lines.Length; r++)
//     {
//         for (var c = 0; c < lines.Length; c++)
//         {
//             map[r,c] = int.Parse(lines[r][c].ToString());
//         }
//     }

//     return map;
// }

// // void printArray(int[,] a)
// // {
// //     for (var r = 0; r < a.GetLength(0); r++)
// //     {
// //         for (var c = 0; c < a.GetLength(0); c++)
// //         {
// //             Console.Write(a[r,c]);
// //         }

// //         Console.WriteLine();
// //     }
// // }