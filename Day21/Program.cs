using AoC.Tools.Models;

namespace Day21;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(64)}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static long Part2()
    {
        var maxSteps = 26501365;
        var gridLength = _input.Length;
        var halfLength = gridLength / 2;

        var steps = Enumerable.Range(0, 3).Select(x => (double)Solve(x * gridLength + halfLength)).ToList();
        var diffs = steps.EnumerateDifferences().ToList();

        double x = maxSteps / gridLength;
        return (long)(steps[0] + (diffs[0] * x) + x * (x - 1) / 2 * (diffs[1] - diffs[0]));
    }

    private static int Solve(int maxSteps)
    {
        var start = FindStart();
        Queue<State> queue = new([new State(start, 0)]);
        HashSet<State> visited = [];
        var gridSize = _input.Length;

        while (queue.TryDequeue(out var current))
        {
            if (!visited.Add(new(current.Position, current.Steps % 2)) || current.Steps == maxSteps)
                continue;

            foreach (var neighbour in current.Position.GetNeighbours())
            {
                if (_input[neighbour.Row.Mod(gridSize)][neighbour.Col.Mod(gridSize)] != '#')
                    queue.Enqueue(new State(neighbour, current.Steps + 1));
            }
        }

        return visited.Count(x => x.Steps == (maxSteps % 2));
    }

    private static Position FindStart()
    {
        for (int row = 0; row < _input.Length; row++)
            if (_input[row].Contains('S'))
                return new Position(_input[row].IndexOf('S'), row);

        return new Position(-1, -1);
    }
}

public record State(Position Position, int Steps);