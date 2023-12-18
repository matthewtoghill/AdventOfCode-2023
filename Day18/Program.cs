using AoC.Tools.Models;

namespace Day18;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static long Part1()
    {
        var current = (0L, 0L);
        var grid = new List<(long, long)> { current };
        var perimeter = 0;

        foreach (var line in _input)
        {
            var split = line.Split([" ", "(", ")"], StringSplitOptions.RemoveEmptyEntries);
            var direction = split[0][0];
            var distance = int.Parse(split[1]);

            current = current.MoveInDirection(direction, distance);
            perimeter += distance;
            grid.Add(current);
        }

        return (grid.CalculateArea() + perimeter) / 2 + 1;
    }

    private static long Part2()
    {
        var current = (0L, 0L);
        var grid = new List<(long, long)> { current };
        var perimeter = 0L;

        foreach (var line in _input)
        {
            var split = line.Split([" ", "(", ")"], StringSplitOptions.RemoveEmptyEntries);
            var colour = split[2];
            var direction = GetDirection(colour[^1]);
            var distance = Convert.ToInt64(colour[1..^1], 16);

            current = current.MoveInDirection(direction, distance);
            perimeter += distance;
            grid.Add(current);
        }


        return (grid.CalculateArea() + perimeter) / 2 + 1;
    }

    private static char GetDirection(char val)
        => val switch
        {
            '0' => 'R',
            '1' => 'D',
            '2' => 'L',
            '3' => 'U',
            _ => throw new NotImplementedException()
        };
}
