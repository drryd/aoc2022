// var lines = File.ReadAllLines("input.txt");

// const int ROW = 0;
// const int COL = 1;

// var grid = createStartingGrid(lines);
// var SIDE_SIZE = grid.GetLength(0);

// // Start in the middle of the grid.
// var middle_position_index = (SIDE_SIZE / 2);
// //grid[middle_position_index,middle_position_index] = "H";

// var head_position = new int[] { middle_position_index, middle_position_index }; // (ROW, COL)
// var tail_position = new int[] { middle_position_index, middle_position_index };

// var spotsTailVisited = new HashSet<(int, int)>();
// spotsTailVisited.Add((tail_position[0], tail_position[1]));

// //grid[head_position[ROW], head_position[COL]] = "H";
// //printArray(grid);
// foreach ( var line in lines )
// {
//     var split_line = line.Split(" ");
//     var direction = split_line[0];
//     var magnitude = int.Parse(split_line[1]);

//     while (magnitude > 0)
//     {
//         //grid[head_position[ROW], head_position[COL]] = "";

//         if (direction == "L")
//         {
//             head_position[COL]--;

//             if ( !isTailTouching(head_position, tail_position) )
//             {
//                 //grid[tail_position[ROW], tail_position[COL]] = "";
//                 tail_position[ROW] = head_position[ROW];
//                 tail_position[COL] = head_position[COL] + 1;
//                 spotsTailVisited.Add((tail_position[ROW], tail_position[COL]));
//             }
//         }
//         if (direction == "R")
//         {
//             head_position[COL]++;

//             if ( !isTailTouching(head_position, tail_position) )
//             {
//                 //grid[tail_position[ROW], tail_position[COL]] = "";
//                 tail_position[ROW] = head_position[ROW];
//                 tail_position[COL] = head_position[COL] - 1;
//                 spotsTailVisited.Add((tail_position[ROW], tail_position[COL]));
//             }
//         }
//         if (direction == "U")
//         {
//             head_position[ROW]--;

//             if ( !isTailTouching(head_position, tail_position) )
//             {
//                 //grid[tail_position[ROW], tail_position[COL]] = "";
//                 tail_position[COL] = head_position[COL];
//                 tail_position[ROW] = head_position[ROW] + 1;
//                 spotsTailVisited.Add((tail_position[ROW], tail_position[COL]));
//             }
//         }
//         if (direction == "D")
//         {
//             head_position[ROW]++;

//             if ( !isTailTouching(head_position, tail_position) )
//             {
//                 //grid[tail_position[ROW], tail_position[COL]] = "";
//                 tail_position[COL] = head_position[COL];
//                 tail_position[ROW] = head_position[ROW] - 1;
//                 spotsTailVisited.Add((tail_position[ROW], tail_position[COL]));
//             }
//         }

//         //grid[head_position[ROW], head_position[COL]] = "H";
//         //grid[tail_position[ROW], tail_position[COL]] = "T";
//         //printArray(grid);

//         magnitude -= 1;
//     }
// }

// bool isTailTouching(int[] head_position, int[] tail_position)
// {
//     // Center
//     var tailOnHead = tail_position[ROW] == head_position[ROW] && tail_position[COL] == head_position[COL];
//     // N Helper
//     var tailOneRowAbove = tail_position[ROW] == head_position[ROW] - 1;
//     // NW
//     var tailTopLeft = tailOneRowAbove && tail_position[COL] == head_position[COL] - 1;
//     // N
//     var tailImmediateUp = tailOneRowAbove && tail_position[COL] == head_position[COL];
//     // NE
//     var tailTopRight = tailOneRowAbove && tail_position[COL] == head_position[COL] + 1;
//     // W
//     var tailImmediateLeft = tail_position[COL] == head_position[COL] - 1 && tail_position[ROW] == head_position[ROW];
//     // E
//     var tailImmediateRight = tail_position[COL] == head_position[COL] + 1 && tail_position[ROW] == head_position[ROW];
//     // S Helper
//     var tailOneRowBelow = tail_position[ROW] == head_position[ROW] + 1;
//     // SW
//     var tailBotLeft = tailOneRowBelow && tail_position[COL] == head_position[COL] - 1;
//     // S
//     var tailImmediateDown = tailOneRowBelow && tail_position[COL] == head_position[COL];
//     // SE
//     var tailBotRight = tailOneRowBelow && tail_position[COL] == head_position[COL] + 1;

//     return tailOnHead || tailTopLeft || tailImmediateUp || tailTopRight || tailImmediateLeft || tailImmediateRight || tailBotLeft || tailImmediateDown || tailBotRight;
// }

// Console.WriteLine(spotsTailVisited.Count);

// // void printArray(string[,] a)
// // {
// //     for (var r = 0; r < a.GetLength(0); r++)
// //     {
// //         for (var c = 0; c < a.GetLength(0); c++)
// //         {
// //             //Console.Write(a[r,c]);
// //             if (a[r,c] == null || a[r,c] == "")
// //             {
// //                 Console.Write("-");
// //             }
// //             else
// //             {
// //                 Console.Write(a[r,c]);
// //             }
// //         }

// //         Console.WriteLine();
// //     }
// //     Console.WriteLine();
// // }

// // Parse the input file to determine how far in every direction we go.
// // Create a grid from this information that will allow us to execute the simulation without going out-of-bounds.
// string[,] createStartingGrid(string[] lines)
// {
//     var max_L_magnitude = 0;
//     var max_R_magnitude = 0;
//     var max_U_magnitude = 0;
//     var max_D_magnitude = 0;

//     var curr_horizontal_shift = 0;
//     var curr_vertical_shift = 0;

//     // Find the maximum extents of the grid
//     foreach (var line in lines)
//     {
//         var split_line = line.Split(" ");
//         var direction = split_line[0];
//         var magnitude = int.Parse(split_line[1]);

//         if ( direction == "L" )
//         {
//             curr_horizontal_shift -= magnitude;
//             if (curr_horizontal_shift < 0)
//             {
//                 max_L_magnitude = Math.Max(max_L_magnitude, Math.Abs(curr_horizontal_shift));
//             }
//         }
//         else if ( direction == "R" )
//         {
//             curr_horizontal_shift += magnitude;
//             if (curr_horizontal_shift > 0)
//             {
//                 max_R_magnitude = Math.Max(max_R_magnitude, curr_horizontal_shift);
//             }
//         }
//         else if ( direction == "U" )
//         {
//             curr_vertical_shift -= magnitude;
//             if (curr_vertical_shift < 0)
//             {
//                 max_U_magnitude = Math.Max(max_U_magnitude, Math.Abs(curr_vertical_shift));
//             }
//         }
//         else if ( direction == "D" )
//         {
//             curr_vertical_shift += magnitude;
//             if (curr_vertical_shift > 0)
//             {
//                 max_D_magnitude = Math.Max(max_D_magnitude, curr_vertical_shift);
//             }
//         }
//     }

//     // Find the direction we need the most room in, and use that amount of space on both sides (since we start in the middle).
//     var widthNeeded = Math.Max(max_L_magnitude, max_R_magnitude) * 2;
//     var heightNeeded = Math.Max(max_U_magnitude, max_D_magnitude) * 2;

//     // Ensure they are an odd number so we can set our starting point to the center of the grid.
//     if (widthNeeded % 2 == 0) { widthNeeded++; }
//     if (heightNeeded % 2 == 0) { heightNeeded++; }

//     // Make it a square
//     var SIDE_SIZE = Math.Max(widthNeeded, heightNeeded);

//     // Add a buffer just to make the tail check easier later -- this way we can assume our tail check will be in bounds later.
//     SIDE_SIZE += 2;

//     return new string[SIDE_SIZE,SIDE_SIZE];
// }