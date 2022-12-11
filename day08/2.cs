var lines = System.IO.File.ReadAllLines("input.txt");
var SIDE_SIZE = lines.Length; // Assuming all sides are equal.

var map = parseMapFromLines(lines);

var scenicScore = new int[SIDE_SIZE, SIDE_SIZE];

computeScenicScore(map, scenicScore);

Console.WriteLine(scenicScore.Cast<int>().Max());

void computeScenicScore(int[,] map, int[,] scenicScore)
{
    // Edges are left as zero because they will have at least one viewing distance of zero.
    for (var r = 1; r < SIDE_SIZE - 1; r++)
    {
        for (var c = 1; c < SIDE_SIZE - 1; c++)
        {
            scenicScore[r, c] = numTreesVisibleToLeft(map, r, c) *
                                numTreesVisibleToRight(map, r, c) *
                                numTreesVisibleAbove(map, r, c) *
                                numTreesVisibleBelow(map, r, c);
        }
    }
}

int numTreesVisibleToLeft(int[,] map, int r, int c)
{
    var numTreesVisible = 0;
    var currTreeHeight = map[r,c];
    while ( c > 0 )
    {
        numTreesVisible += 1;

        if ( map[r, c-1] >= currTreeHeight )
        {
            break;
        }

        c -= 1;
    }

    return numTreesVisible;
}

int numTreesVisibleToRight(int[,] map, int r, int c)
{
    var numTreesVisible = 0;
    var currTreeHeight = map[r,c];
    while ( c < SIDE_SIZE - 1 )
    {
        numTreesVisible += 1;

        if ( map[r, c+1] >= currTreeHeight )
        {
            break;
        }

        c += 1;
    }

    return numTreesVisible;
}

int numTreesVisibleAbove(int[,] map, int r, int c)
{
    var numTreesVisible = 0;
    var currTreeHeight = map[r,c];
    while ( r > 0 )
    {
        numTreesVisible += 1;

        if ( map[r-1, c] >= currTreeHeight )
        {
            break;
        }

        r -= 1;
    }

    return numTreesVisible;
}

int numTreesVisibleBelow(int[,] map, int r, int c)
{
    var numTreesVisible = 0;
    var currTreeHeight = map[r,c];
    while ( r < SIDE_SIZE - 1 )
    {
        numTreesVisible += 1;
        
        if ( map[r+1, c] >= currTreeHeight )
        {
            break;
        }

        r += 1;
    }

    return numTreesVisible;
}

int[,] parseMapFromLines(string[] lines)
{
    var map = new int[lines.Length, lines.Length];

    for (var r = 0; r < lines.Length; r++)
    {
        for (var c = 0; c < lines.Length; c++)
        {
            map[r,c] = int.Parse(lines[r][c].ToString());
        }
    }

    return map;
}

// void printArray(int[,] a)
// {
//     for (var r = 0; r < a.GetLength(0); r++)
//     {
//         for (var c = 0; c < a.GetLength(0); c++)
//         {
//             Console.Write(a[r,c]);
//             Console.Write("  ");
//         }

//         Console.WriteLine();
//     }
// }