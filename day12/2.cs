const char START_POS = 'S';
const char END_POS = 'E';
const int MAX_ELEVATION_CHANGE = 1;

var map = File.ReadAllLines("input.txt");
var NUM_ROWS = map.Length;
var NUM_COLS = map[0].Length;

var startPositions = findStartPositions(map);
var minDistance = NUM_ROWS * NUM_COLS;

foreach (var startPosition in startPositions)
{
    Queue<Path> frontier = new();
    frontier.Enqueue(new Path(startPosition, 0));
    HashSet<Position> visited = new();
    visited.Add(startPosition);

    while (frontier.Count > 0)
    {
        var nextPathToCheck = frontier.Dequeue();

        if (map[nextPathToCheck.currPosition.row][nextPathToCheck.currPosition.col] == END_POS)
        {
            minDistance = Math.Min(minDistance, nextPathToCheck.distanceSoFar);
        }
        
        var left = nextPathToCheck.currPosition.left();
        var right = nextPathToCheck.currPosition.right();
        var up = nextPathToCheck.currPosition.up();
        var down = nextPathToCheck.currPosition.down();

        if (isValidMove(nextPathToCheck.currPosition, left) && !visited.Contains(left))
        {
            frontier.Enqueue(new Path(left, nextPathToCheck.distanceSoFar + 1));
            visited.Add(left);
        }
        if (isValidMove(nextPathToCheck.currPosition, right) && !visited.Contains(right))
        {
            frontier.Enqueue(new Path(right, nextPathToCheck.distanceSoFar + 1));
            visited.Add(right);
        }
        if (isValidMove(nextPathToCheck.currPosition, up) && !visited.Contains(up))
        {
            frontier.Enqueue(new Path(up, nextPathToCheck.distanceSoFar + 1));
            visited.Add(up);
        }
        if (isValidMove(nextPathToCheck.currPosition, down) && !visited.Contains(down))
        {
            frontier.Enqueue(new Path(down, nextPathToCheck.distanceSoFar + 1));
            visited.Add(down);
        }
    }
}

Console.WriteLine(minDistance);

// Returns true iff p is on the map and wouldn't be too steep to move to
bool isValidMove(Position currPosition, Position newPosition)
{
    bool validRow = newPosition.row >= 0 && newPosition.row < NUM_ROWS;
    bool validCol = newPosition.col >= 0 && newPosition.col < NUM_COLS;
    return validRow && validCol && notTooSteep(map[currPosition.row][currPosition.col], map[newPosition.row][newPosition.col]);
}

List<Position> findStartPositions(string[] map)
{
    List<Position> startPositions = new();
    for (var r = 0; r < NUM_ROWS; r++)
    {
        for (var c = 0; c < NUM_COLS; c++)
        {
            if (map[r][c] == START_POS || map[r][c] == 'a')
            {
                startPositions.Add(new Position(r, c));
            }
        }
    }

    return startPositions;
}

//Returns true if we are able to move from c1 to c2.
bool notTooSteep(char c1, char c2)
{
    if (c1 == START_POS)
    {
        return c2 == 'a' || c2 == 'b';
    }

    if (c2 == END_POS)
    {
        return c1 == 'y' || c1 == 'z';
    }

    return c2 - c1 <= MAX_ELEVATION_CHANGE;
}

public struct Position
{
    public int row;
    public int col;

    public Position(int row = 0, int col = 0)
    {
        this.row = row;
        this.col = col;
    }

    public void print()
    {
        Console.WriteLine($"Row: {row}, Col: {col}");
    }

    public Position left()
    {
        return new Position(row, col - 1);
    }

    public Position right()
    {
        return new Position(row, col + 1);
    }

    public Position up()
    {
        return new Position(row - 1, col);
    }

    public Position down()
    {
        return new Position(row + 1, col);
    }
}

public struct Path
{
    public Position currPosition;
    public int distanceSoFar;

    public Path(Position currPosition, int distanceSoFar)
    {
        this.currPosition = currPosition;
        this.distanceSoFar = distanceSoFar;
    }
}