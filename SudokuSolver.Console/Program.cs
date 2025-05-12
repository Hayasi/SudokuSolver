using SudokuSolver;

if (args.Length > 0)
{
    if (args[0].Length != 9*9)
    {
        Console.Error.WriteLine("Invalid argument");
        Environment.Exit(1);
    }

    try
    {
        var solved = Sudoku.Solve(args[0].Select(a => int.Parse(a.ToString())).ToArray());
        if (solved == null)
        {
            Console.Error.WriteLine("Impossible to solve");
            Environment.Exit(2);
        }

        Console.WriteLine(string.Join("", solved));
    }
    catch
    {
        Console.Error.WriteLine("Invalid argument");
        Environment.Exit(1);
    }

    return;
}

Console.WriteLine("Input sudoku. Use 0 for empty cells.");

var n = 0;
var sudoku = new int[9*9];
while (n < 9*9)
{
    var input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
        foreach (var c in input)
        {
            if (n >= 9*9) break;
            if (char.IsDigit(c))
            {
                sudoku[n++] = int.Parse(c.ToString());
            }
        }
}

Console.WriteLine();

var result = Sudoku.Solve(sudoku);
if (result == null)
{
    Console.WriteLine("Unsolvable!");
    return;
}

for (int i = 0; i < 9; i++)
{
    if (i > 0 && i % 3 == 0)
    {
        Console.WriteLine("-----------------");
    }
    for (int j = 0; j < 9; j++)
    {
        Console.Write(result[i * 9 + j]);
        if (j != 8)
        {
            if (j % 3 == 2) Console.Write("|");
            else Console.Write(" ");
        }
    }
    Console.WriteLine();
}
