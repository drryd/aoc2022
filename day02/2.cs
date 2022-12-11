using System;
using System.IO;

string[] lines = File.ReadAllLines("input.txt");

var totalScore = 0;

foreach (var line in lines)
{
    var (opponentMove, roundOutcome) = line.Split(' ') switch
    {
        [var o, var r] => (o, r),
        _ => throw new Exception()
    };

    totalScore += scoreRound(opponentMove, roundOutcome);
}

Console.WriteLine(totalScore);

int scoreRound(string opponentMove, string roundOutcome)
{
    return scoreMyMove(determineMyMove(opponentMove, roundOutcome)) + scoreOutcome(roundOutcome);
}

int scoreOutcome(string roundOutcome) => roundOutcome switch
{
    "X" => 0,   // Lose
    "Y" => 3,   // Draw
    "Z" => 6,   // Win
    _ => throw new Exception()
};

int scoreMyMove(string myMove) => myMove switch
{
    "R" => 1,
    "P" => 2,
    "S" => 3,
    _ => throw new Exception()
};

string determineMyMove(string opponentMove, string roundOutcome) => (opponentMove, roundOutcome) switch
{
    ("A", "X") => "S",
    ("A", "Y") => "R",
    ("A", "Z") => "P",
    ("B", "X") => "R",
    ("B", "Y") => "P",
    ("B", "Z") => "S",
    ("C", "X") => "P",
    ("C", "Y") => "S",
    ("C", "Z") => "R",
    _ => throw new Exception()
};
