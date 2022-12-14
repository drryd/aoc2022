var lines = File.ReadAllLines("input.txt");

const int NUM_ITERATIONS = 10000;

var monkeys = parseMonkeysFromInput(lines);

long maxResult = 1;
foreach (var monkey in monkeys)
{
    maxResult *= monkey.testDivisibleBy;
}

for (var i = 0; i < NUM_ITERATIONS; i++)
{
    for (var monkey_i = 0; monkey_i < monkeys.Count; monkey_i++)
    {
        foreach (var item in monkeys[monkey_i].items)
        {
            monkeys[monkey_i].numItemsInspected++;
            var operationResult = monkeys[monkey_i].operation.apply(item);
            // Note we compute the maximum possible result size above as the product of all monkey testDivisibleBy's - any information larger than this number is not useful.
            // Ex:  Consider one monkey with a divisibility check of 2.
            //      It doesn't matter we are checking 4, 6, 8, 57829052, etc -- any number above 2 doesn't matter as we can keep taking away two until it's either a 0 or 1 to get our answer.
            //      If we only had this one monkey, we could always reduce the number we are checking by 2 until we are in the range [0-1].
            //      
            //      Then consider one monkey with a divisibility check of 5.
            //      For numbers, 10, 25, 1970, etc, we can keep removing 5 until we get 0, 1, 2, 3, or 4 for our answer.
            //      If we only had this monkey, we could always reduce the number by 5 until we are in the range [0-4].
            //      Note that another way to perform this reduction operation would be to take num % 5.
            //
            //      Now consider two monkeys with divisibility checks of 2 and 5.
            //      If we continued to reduce by 2 or 5 as before, we could hit some problems.
            //      Consider the number 8. This number is not divisible by 5 but is divisible by 2. If we repeatedly reduced by 2 until it was in the range [0-1] as before, we would get 0, which is divisible by 5.
            //      Similarly, the number 15 is not divisible by 2 but is divisible by 5. If we repeatedly reduced by 5 until it was in the range [0-4] as before, we would get 0, which is divisible by 2.
            //      So we cannot use either 2 or 5 as a reduction number without breaking our divisibility checks.
            //
            //      What if we took the product of the divisibility checks (2*5=10) and reduced by this value if we were above it?
            //      In the case of 15 -- if we reduced by 10 we will have a remainder of 5, which is divisible by 5 and is not divisible by 2.
            //      This respects the results of the divisibility checks while reducing the value we need to check.
            //
            //      Consider the results of % 10 for the following
            //      0 -> 0
            //      1 -> 1
            //      2 -> 2
            //      ...
            //      9 -> 9
            //      10 -> 0
            //      11 -> 1
            //      12 -> 2
            //      ...
            //      99 -> 9
            //
            //      We can mathematically write this x -> y form as:
            //      x % (n * m) = y
            //      Note that y is bound in the range [0,(n*m)-1]
            //
            //      We can then see that y % n is equivalent to x % n (ex: 9mod2 = 99mod2 = 1)
            //      and similarly y % m is equivalent to x % m        (ex: 9mod5 = 99mod5 = 4)
            //      This means we can use y (which has an upper bound of n*m) in place of x (which is effectively unbounded).
            //      
            // In the code below, we have operationResult %= maxResult
            // This can be read as "use the reduced version of operationResult" (use y instead of x in the example above)
            //
            // Also as noted above, an equivalent way to reduce would be to repeatedly subtract maxResult until we are within that maximum, ex:
            // while (operationResult > maxResult)
            // {
            //     operationResult -= maxResult;
            // }
            operationResult %= maxResult;
            if (operationResult % monkeys[monkey_i].testDivisibleBy == 0)
            {
                var monkeyToPassTo = monkeys[monkeys[monkey_i].ifTestTrue];
                monkeyToPassTo.addItem(operationResult);
            }
            else
            {
                var monkeyToPassTo = monkeys[monkeys[monkey_i].ifTestFalse];
                monkeyToPassTo.addItem(operationResult);
            }
        }

        // Assuming monkeys can't toss to themselves.
        monkeys[monkey_i].clearItems();
    }
}

var sortedMonkeys = monkeys.OrderBy( o => -o.numItemsInspected ).ToList();
Console.WriteLine(sortedMonkeys[0].numItemsInspected * sortedMonkeys[1].numItemsInspected);

List<Monkey> parseMonkeysFromInput(string[] input)
{
    var monkeys = new List<Monkey>();

    var i = 0;
    while (i < input.Length)
    {
        // Monkey n:
        var id = i / 7;
        i++;

        // Starting items
        var startingItemsStr = input[i].Split(": ")[1].Split(", ");
        List<long> startingItems = new();
        foreach (var startingItemStr in startingItemsStr)
        {
            startingItems.Add(long.Parse(startingItemStr));
        }
        i++;

        // Operation - assume only one operator and two operands, one of which is "old".
        //             Only +, *, and pow2 operations are allowed
        var operationRaw = input[i].Split(": new = old ")[1];
        Operation operation = new Operation();
        if (operationRaw.EndsWith("* old"))
        {
            operation.operator_ = Operator.POW;
            operation.operand_  = 2;
        }
        else if (operationRaw.Contains("*"))
        {
            operation.operator_ = Operator.MULT;
            operation.operand_ = int.Parse(operationRaw.Split("* ")[1]);
        }
        else if (operationRaw.Contains("+"))
        {
            operation.operator_ = Operator.ADD;
            operation.operand_ = int.Parse(operationRaw.Split("+ ")[1]);
        }
        i++;
        
        // Test
        var testDivisibleBy = int.Parse(input[i].Split(" by ")[1]);
        i++;

        // if true
        var ifTestTrue = int.Parse(input[i].Split(" monkey ")[1]);
        i++;
        
        // if false
        var ifTestFalse = int.Parse(input[i].Split(" monkey ")[1]);
        i++;

        monkeys.Add(new Monkey(id, startingItems, operation, testDivisibleBy, ifTestTrue, ifTestFalse));
        
        // Blank line
        i++;
    }
    
    return monkeys;
}

public class Operation
{
    public Operator operator_;
    public long operand_;
    public long apply(long x)
    {
        switch (operator_)
        {
            case Operator.ADD:
                return operand_ + x;
            case Operator.MULT:
                return operand_ * x;
            case Operator.POW:
                return (long)Math.Pow(x, operand_);
            default:
                throw new Exception();
        }
    }
}

public enum Operator
{
    ADD = 0,
    MULT = 1,
    POW = 2
}

class Monkey
{
    public int id { get; }
    public List<long> items { get; private set; }
    public Operation operation { get; }
    public int testDivisibleBy { get; }
    public int ifTestTrue { get; }
    public int ifTestFalse { get; }
    public long numItemsInspected { get; set; }

    public Monkey(int id, List<long> items, Operation operation, int testDivisibleBy, int ifTestTrue, int ifTestFalse)
    {
        this.id = id;
        this.items = items;
        this.operation = operation;
        this.testDivisibleBy = testDivisibleBy;
        this.ifTestTrue = ifTestTrue;
        this.ifTestFalse = ifTestFalse;
        this.numItemsInspected = 0;
    }

    public void clearItems()
    {
        this.items.Clear();
    }

    public void addItem(long x)
    {
        this.items.Add(x);
    }
}