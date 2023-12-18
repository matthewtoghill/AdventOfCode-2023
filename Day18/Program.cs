using AoC.Tools.Models;

namespace Day18;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(_input.Select(Part1Parser).ToList())}");
        Console.WriteLine($"Part 2: {Solve(_input.Select(Part2Parser).ToList())}");
    }

    private static long Solve(List<Dig> instructions)
    {
        (long, long) current = (0, 0);
        List<(long, long)> grid = [current];
        long perimeter = 0;

        foreach (var item in instructions)
        {
            current = current.MoveInDirection(item.Direction, item.Distance);
            perimeter += item.Distance;
            grid.Add(current);
        }

        return (grid.CalculateArea() + perimeter) / 2 + 1;
    }

    private static Dig Part1Parser(string line)
    {
        var split = line.Split([" ", "(", ")"], StringSplitOptions.RemoveEmptyEntries);
        return new(split[0][0], long.Parse(split[1]));
    }

    private static Dig Part2Parser(string line)
    {
        var colour = line.Split([" ", "(", ")"], StringSplitOptions.RemoveEmptyEntries)[2];
        return new(GetDirection(colour[^1]), Convert.ToInt64(colour[1..^1], 16));
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

public record Dig(char Direction, long Distance);