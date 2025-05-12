namespace SudokuSolver.Tests;

public class SudokuTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 })]
    [InlineData(new int[] {
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, 9,
        1, 2, 3, 4, 5, 6, 7, 8, -1 })]
    public void InvalidArgument_ThrowsException(int[] value)
    {
        Assert.Throws<ArgumentException>(() => Sudoku.Solve(value));
    }

    [Theory]
    [ClassData(typeof(SudokuTestsData))]
    public void Sudoku_Solve(int[] value, int[] expected)
    {
        Assert.Equal(expected, Sudoku.Solve(value));
    }
}
