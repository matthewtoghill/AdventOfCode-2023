using System.Text;
using AoC.Tools.Models;

namespace Day03;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1()
    {
        var partNumbers = new List<int>();
        HashSet<Position> checkedPositions = [];

        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col < _input[row].Length; col++)
            {
                var c = _input[row][col];
                if (c == '.' || char.IsDigit(c))
                    continue;

                foreach (var (nRow, nCol) in new Position(row, col).GetNeighbours(true))
                {
                    if (!char.IsDigit(_input[nRow][nCol]))
                        continue;

                    var partNumber = RetrieveNumber(_input[nRow], nRow, nCol, checkedPositions);

                    if (partNumber > 0)
                        partNumbers.Add(partNumber);
                }
            }
        }

        return partNumbers.Sum();
    }

    private static int Part2()
    {
        var gearRatios = new List<int>();
        HashSet<Position> checkedPositions = [];

        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col < _input[row].Length; col++)
            {
                if (_input[row][col] != '*')
                    continue;

                var partNumbers = new List<int>();

                foreach (var (nRow, nCol) in new Position(row, col).GetNeighbours(true))
                {
                    if (!char.IsDigit(_input[nRow][nCol]))
                        continue;

                    var partNumber = RetrieveNumber(_input[nRow], nRow, nCol, checkedPositions);

                    if (partNumber > 0)
                        partNumbers.Add(partNumber);
                }

                if (partNumbers.Count == 2)
                    gearRatios.Add(partNumbers.Product());
            }
        }

        return gearRatios.Sum();
    }

    private static int RetrieveNumber(string line, int row, int col, HashSet<Position> checkedPositions)
    {
        var result = new StringBuilder();

        var newCol = col;
        while (newCol >= 0 && char.IsDigit(line[newCol]))
        {
            Position p = new(newCol, row);
            if (checkedPositions.Contains(p)) return 0;
            result.Insert(0, line[newCol]);
            checkedPositions.Add(p);
            newCol--;
        }

        newCol = col + 1;
        while (newCol < line.Length && char.IsDigit(line[newCol]))
        {
            Position p = new(newCol, row);
            if (checkedPositions.Contains(p)) return 0;
            result.Append(line[newCol]);
            checkedPositions.Add(p);
            newCol++;
        }

        return int.Parse(result.ToString());
    }
}
