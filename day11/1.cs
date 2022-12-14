// var lines = File.ReadAllLines("input.txt");

// var monkeys = parseMonkeysFromInput(lines);

// for (var i = 0; i < 20; i++)
// {
//     for (var monkey_i = 0; monkey_i < monkeys.Count; monkey_i++)
//     {
//         foreach (var item in monkeys[monkey_i].items)
//         {
//             monkeys[monkey_i].numItemsInspected++;
//             var operationResult = monkeys[monkey_i].operation.apply(item);
//             operationResult /= 3;
//             if (operationResult % monkeys[monkey_i].testDivisibleBy == 0)
//             {
//                 monkeys[monkeys[monkey_i].ifTestTrue].addItem(operationResult);
//             }
//             else
//             {
//                 monkeys[monkeys[monkey_i].ifTestFalse].addItem(operationResult);
//             }
//         }

//         // Assuming monkeys can't toss to themselves.
//         monkeys[monkey_i].clearItems();
//     }
// }

// monkeys.Sort((a, b) => b.numItemsInspected - a.numItemsInspected);
// Console.WriteLine(monkeys[0].numItemsInspected * monkeys[1].numItemsInspected);

// List<Monkey> parseMonkeysFromInput(string[] input)
// {
//     var monkeys = new List<Monkey>();

//     var i = 0;
//     while (i < input.Length)
//     {
//         // Monkey n:
//         var id = i / 7;
//         i++;

//         // Starting items
//         var startingItemsStr = input[i].Split(": ")[1].Split(", ");
//         List<int> startingItems = new();
//         foreach (var startingItemStr in startingItemsStr)
//         {
//             startingItems.Add(int.Parse(startingItemStr));
//         }
//         i++;

//         // Operation - assume only one operator and two operands, one of which is "old".
//         //             Only +, *, and pow2 operations are allowed
//         var operationRaw = input[i].Split(": new = old ")[1];
//         Operation operation = new Operation();
//         if (operationRaw.EndsWith("* old"))
//         {
//             operation.operator_ = Operator.POW;
//             operation.operand_  = 2;
//         }
//         else if (operationRaw.Contains("*"))
//         {
//             operation.operator_ = Operator.MULT;
//             operation.operand_ = int.Parse(operationRaw.Split("* ")[1]);
//         }
//         else if (operationRaw.Contains("+"))
//         {
//             operation.operator_ = Operator.ADD;
//             operation.operand_ = int.Parse(operationRaw.Split("+ ")[1]);
//         }
//         i++;
        
//         // Test
//         var testDivisibleBy = int.Parse(input[i].Split(" by ")[1]);
//         i++;

//         // if true
//         var ifTestTrue = int.Parse(input[i].Split(" monkey ")[1]);
//         i++;
        
//         // if false
//         var ifTestFalse = int.Parse(input[i].Split(" monkey ")[1]);
//         i++;

//         monkeys.Add(new Monkey(id, startingItems, operation, testDivisibleBy, ifTestTrue, ifTestFalse));
        
//         // Blank line
//         i++;
//     }
    
//     return monkeys;
// }

// public class Operation
// {
//     public Operator operator_;
//     public int operand_;
//     public int apply(int x)
//     {
//         switch (operator_)
//         {
//             case Operator.ADD:
//                 return operand_ + x;
//             case Operator.MULT:
//                 return operand_ * x;
//             case Operator.POW:
//                 return (int)Math.Pow(x, operand_);
//             default:
//                 throw new Exception();
//         }
//     }
// }

// public enum Operator
// {
//     ADD = 0,
//     MULT = 1,
//     POW = 2
// }

// class Monkey
// {
//     public int id { get; }
//     public List<int> items { get; private set; }
//     public Operation operation { get; }
//     public int testDivisibleBy { get; }
//     public int ifTestTrue { get; }
//     public int ifTestFalse { get; }
//     public int numItemsInspected { get; set; }

//     public Monkey(int id, List<int> items, Operation operation, int testDivisibleBy, int ifTestTrue, int ifTestFalse)
//     {
//         this.id = id;
//         this.items = items;
//         this.operation = operation;
//         this.testDivisibleBy = testDivisibleBy;
//         this.ifTestTrue = ifTestTrue;
//         this.ifTestFalse = ifTestFalse;
//         this.numItemsInspected = 0;
//     }

//     public void clearItems()
//     {
//         this.items.Clear();
//     }

//     public void addItem(int x)
//     {
//         this.items.Add(x);
//     }
// }