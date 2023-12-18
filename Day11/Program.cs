using AoC.Tools.Models;

namespace Day11;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(1)}");
        Console.WriteLine($"Part 2: {Solve(999_999)}");
    }

    private static long Solve(int expandAmount)
    {
        var galaxies = GetGalaxies();
        var emptyRows = GetEmptyRows(galaxies);
        var emptyCols = GetEmptyCols(galaxies);

        long totalDist = 0;

        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                totalDist += GetDistance(galaxies[i], galaxies[j], emptyRows, emptyCols, expandAmount);
            }
        }

        return totalDist;
    }

    private static List<Position> GetGalaxies()
    {
        var galaxies = new List<Position>();

        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col < _input[row].Length; col++)
            {
                if (_input[row][col] == '#')
                {
                    galaxies.Add(new Position(col, row));
                }
            }
        }

        return galaxies;
    }

    private static HashSet<int> GetEmptyRows(List<Position> galaxies) => Enumerable.Range(0, _input.Length).Except(galaxies.Select(x => x.Row)).ToHashSet();
    private static HashSet<int> GetEmptyCols(List<Position> galaxies) => Enumerable.Range(0, _input[0].Length).Except(galaxies.Select(x => x.Col)).ToHashSet();

    private static long GetDistance(Position a, Position b, HashSet<int> rows, HashSet<int> cols, long expandAmount)
    {
        long distance = a.ManhattanDistance(b);

        for (int col = Math.Min(a.Col, b.Col); col < Math.Max(a.Col, b.Col); col++)
        {
            if (cols.Contains(col)) distance += expandAmount;
        }

        for (int row = Math.Min(a.Row, b.Row); row < Math.Max(a.Row, b.Row); row++)
        {
            if (rows.Contains(row)) distance += expandAmount;
        }

        return distance;
    }
}
