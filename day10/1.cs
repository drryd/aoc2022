// var instructions = File.ReadAllLines("input.txt");

// CPU cpu = new();

// foreach (var instruction in instructions)
// {
//     if ( instruction == "noop" )
//     {
//         cpu.execute(CPUOperation.NoOp);
//     }
//     else
//     {
//         var ops = instruction.Split(" ");
        
//         var operator_ = ops[0];
//         var operand_ = int.Parse(ops[1]);

//         if (operator_ == "addx")
//         {
//             cpu.execute(CPUOperation.AddX, operand_);
//         }
//     }
// }

// Console.WriteLine(cpu.interesting_signal_strengths);

// public enum CPUOperation
// {
//     NoOp,
//     AddX
// }

// public class CPU
// {
//     public int register_value { get; private set; }
//     public int cycles_completed { get; private set; }
//     public int interesting_signal_strengths { get; private set; }
//     private HashSet<int> interesting_cycles;

//     public CPU()
//     {
//         cycles_completed = 0;
//         register_value = 1;
//         interesting_signal_strengths = 0;
//         interesting_cycles = new () { 20, 60, 100, 140, 180, 220 };
//     }

//     public void execute(CPUOperation operation, int operand = 0)
//     {
//         switch (operation)
//         {
//             case CPUOperation.NoOp:
//                 noop();
//                 break;
//             case CPUOperation.AddX:
//                 addx(operand);
//                 break;
//             default:
//                 throw new Exception("Unsupported operation");
//         }
//     }

//     private void noop()
//     {
//         tick();
//     }

//     private void addx(int operand)
//     {
//         tick();
//         tick();
//         register_value += operand;
//     }

//     private void tick()
//     {
//         cycles_completed += 1;

//         if ( interesting_cycles.Contains(cycles_completed) )
//         {
//             interesting_signal_strengths += register_value * cycles_completed;
//         }
//     }
// }