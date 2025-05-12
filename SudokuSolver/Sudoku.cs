namespace SudokuSolver;

public class Sudoku
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="field"></param>
    /// <returns>Returns null if sudoku is unsolvable</returns>
    public static int[]? Solve(int[] sudoku)
    {
        if (sudoku == null)
            throw new ArgumentNullException();
        if (sudoku.Length != 9 * 9)
            throw new ArgumentException("Array length should be 81");
        if (sudoku.Any(a => a is <0 or >9))
            throw new ArgumentException("Values should be between 0 and 9");

        return new Sudoku(sudoku).Run();
    }

    private readonly int[] field = new int[9*9];

    private Sudoku(int[] field)
    {
        Array.Copy(field, this.field, this.field.Length);
    }

    private int[]? Run()
    {
        var state = DetermineState(this.field);
        if (state == State.Solved) return this.field;
        if (state == State.Impossible) return null;

        var result = SetValue(field, 0);
        if (result == null) return null;
        if (DetermineState(result) == State.Solved)
            return result;

        return null;
    }

    private int[]? SetValue(int[] field, int startIndex)
    {
        int[] newField = (int[])field.Clone();
        int i = startIndex;
        while (i < field.Length && field[i] != 0) i++;
        if (i == field.Length) return newField;

        for (int j = 1; j <= 9; j++)
        {
            if (CheckRow(newField, i, j)
                && CheckColumn(newField, i, j)
                && CheckSection(newField, i, j))
            {
                newField[i] = j;
                var result = SetValue(newField, i + 1);
                if (result != null) return result;
            }
        }

        return null;
    }

    private State DetermineState(int[] field)
    {
        bool hasZero = false;
        var rowSet = new HashSet<int>();
        var columnSet = new HashSet<int>();
        var sectionSet = new HashSet<int>();
        for (int i = 0; i < 9; i++)
        {
            rowSet.Clear();
            columnSet.Clear();
            sectionSet.Clear();

            for (int j = 0; j < 9; j++)
            {
                var v = field[GetIndex(i, j)];
                if (v == 0) hasZero = true;
                else if (!rowSet.Add(v)) return State.Impossible;
                v = field[GetIndex(j, i)];
                if (v == 0) hasZero = true;
                else if (!columnSet.Add(v)) return State.Impossible;
                v = field[GetSectionIndex(i, j)];
                if (v == 0) hasZero = true;
                else if (!sectionSet.Add(v)) return State.Impossible;
            }
        }
        return hasZero ? State.Unsolved : State.Solved;
    }

    private int GetIndex(int row, int column)
    {
        return row * 9 + column;
    }

    private int GetSectionIndex(int section, int cell)
    {
        return section / 3 * 3 * 9 + section % 3 * 3 + cell / 3 * 9 + cell % 3;
    }

    private bool CheckRow(int[] field, int index, int value)
    {
        int row = index / 9;
        for (int i = 0; i < 9; i++)
        {
            var v = field[GetIndex(row, i)];
            if (v == value) return false;
        }
        return true;
    }

    private bool CheckColumn(int[] field, int index, int value)
    {
        int column = index % 9;
        for (int i = 0; i < 9; i++)
        {
            var v = field[GetIndex(i, column)];
            if (v == value) return false;
        }
        return true;
    }

    private bool CheckSection(int[] field, int index, int value)
    {
        int section = index / 9 / 3 * 3 + index % 9 / 3;
        for (int i = 0; i < 9; i++)
        {
            var v = field[GetSectionIndex(section, i)];
            if (v == value) return false;
        }
        return true;
    }
}

internal enum State
{
    Unsolved,
    Solved,
    Impossible
}
