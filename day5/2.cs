using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("input.txt");

int divider_index = findDividerIndex(lines);
var state = readInitialStackState(lines, divider_index);
executeOperations(state, lines, divider_index);
printTopOfStacks(state);

// Find the index of the empty line that divides the initial state from the operation list
int findDividerIndex(string[] lines)
{
    for (var divider_index = 0; divider_index < lines.Length; divider_index++)
    {
        if (lines[divider_index].Length == 0)
        {
            return divider_index;
        }
    }

    throw new Exception();
}

// Parse the initial stack state.
List<Stack<char>> readInitialStackState(string[] lines, int divider_index)
{
    List<Stack<char>> stacks = new();

    for ( var i = 0; i < lines[divider_index - 1].Length; i++ )
    {
        if (lines[divider_index - 1][i] == ' ')
        {
            continue;
        }

        Stack<char> s = new();

        for ( var r = divider_index - 2; r >= 0; r-- )
        {
            if ( lines[r][i] == ' ')
            {
                break;
            }

            s.Push(lines[r][i]);
        }

        stacks.Add(s);
    }

    return stacks;
}

// Apply the state changes specified in lines after the divider.
void executeOperations(List<Stack<char>> state, string[] lines, int divider_index)
{
    string pattern = @"\d+";

    for (var i = divider_index + 1; i < lines.Length; i++)
    {
        MatchCollection m = Regex.Matches(lines[i], pattern);

        var (amountToMove, fromStack, toStack) = (int.Parse(m[0].Value), int.Parse(m[1].Value) - 1, int.Parse(m[2].Value) - 1);

        Stack<char> elementsToMove = new();
        while (amountToMove > 0 && state[fromStack].Count > 0)
        {
            elementsToMove.Push(state[fromStack].Pop());
            amountToMove -= 1;
        }

        while (elementsToMove.Count > 0)
        {
            state[toStack].Push(elementsToMove.Pop());
        }
    }
}

// Print the top value of each stack (if there is one).
void printTopOfStacks(List<Stack<char>> state)
{
    Console.WriteLine(state[0].Count);
    foreach (var stack in state)
    {
        if (stack.Count > 0)
        {
            Console.Write(stack.Pop());
        }
    }
}