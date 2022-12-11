var instructions = File.ReadAllLines("input.txt");

CRT crt = new();
CPU cpu = new(crt);

foreach (var instruction in instructions)
{
    if ( instruction == "noop" )
    {
        cpu.execute(CPUOperation.NoOp);
    }
    else
    {
        var ops = instruction.Split(" ");
        
        var operator_ = ops[0];
        var operand_ = int.Parse(ops[1]);

        if (operator_ == "addx")
        {
            cpu.execute(CPUOperation.AddX, operand_);
        }
    }
}

crt.flushToScreen();

public enum CPUOperation
{
    NoOp,
    AddX
}

public class CPU
{
    public int register_value { get; private set; }
    public int cycles_completed { get; private set; }
    private CRT crt;

    public CPU(CRT crt)
    {
        cycles_completed = 0;
        register_value = 1;
        this.crt = crt;
    }

    private void noop()
    {
        tick();
    }

    private void addx(int operand)
    {
        tick();
        tick();
        register_value += operand;
    }

    public void execute(CPUOperation operation, int operand = 0)
    {
        switch (operation)
        {
            case CPUOperation.NoOp:
                noop();
                break;
            case CPUOperation.AddX:
                addx(operand);
                break;
            default:
                throw new Exception("Unsupported operation");
        }
    }

    private void tick()
    {
        crt.drawPixel(register_value, cycles_completed);
        cycles_completed += 1;
    }
}

public class CRT
{
    private const int NUM_ROWS = 6;
    private const int NUM_COLS = 40;
    private string[] buffer = new string[NUM_ROWS * NUM_COLS];
    private HashSet<int> sprite_positions = new();

    public CRT()
    {
        // Initialize all pixels to dark.
        for (var i = 0; i < NUM_ROWS * NUM_COLS; i++)
        {
            buffer[i] = ".";
        }
    }

    public void drawPixel(int register_value, int cycles_completed)
    {
        sprite_positions = new HashSet<int>() { register_value - 1, register_value, register_value + 1 };

        if (sprite_positions.Contains(cycles_completed % NUM_COLS))
        {
            buffer[cycles_completed] = "#";
        }
    }

    public void flushToScreen()
    {
        for (var i = 0; i < NUM_ROWS * NUM_COLS; i++)
        {
            if (i > 0 && i % NUM_COLS == 0)
            {
                Console.WriteLine();
            }

            Console.Write(buffer[i]);
        }
    }
}
