// using System;
// using System.IO;

// string[] lines = File.ReadAllLines("input.txt");

// var totalScore = 0;

// foreach (var line in lines)
// {
//     var (opponentMove, myMove) = line.Split(' ') switch
//     {
//         [var o, var m] => (o, m),
//         _ => throw new Exception()
//     };

//     totalScore += scoreRound(opponentMove, myMove);
// }

// Console.WriteLine(totalScore);

// int scoreWinResult(WinResult winResult) => winResult switch
// {
//     WinResult.Win => 6,
//     WinResult.Loss => 0,
//     WinResult.Draw => 3,
//     _ => throw new Exception()
// };

// int scoreMove(string myMove) => myMove switch
// {
//     "X" => 1,
//     "Y" => 2,
//     "Z" => 3,
//     _ => throw new Exception()
// };

// int scoreRound(string opponentMove, string myMove)
// {
//     return scoreWinResult(determineWinResult(opponentMove, myMove)) + scoreMove(myMove);
// }

// WinResult determineWinResult(string opponentMove, string myMove) => (opponentMove, myMove) switch
// {
//     ("A", "X") => WinResult.Draw,
//     ("A", "Y") => WinResult.Win,
//     ("A", "Z") => WinResult.Loss,
//     ("B", "X") => WinResult.Loss,
//     ("B", "Y") => WinResult.Draw,
//     ("B", "Z") => WinResult.Win,
//     ("C", "X") => WinResult.Win,
//     ("C", "Y") => WinResult.Loss,
//     ("C", "Z") => WinResult.Draw,
//     _ => throw new Exception()
// };

// enum WinResult
// {
//     Win,
//     Loss,
//     Draw
// }