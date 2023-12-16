using AoC.Tools.Models;

namespace Day16;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1() => CountEnergized(0, -1, 'E');

    private static int Part2()
    {
        var energizedCounts = new List<int>();

        // down the rows, top to bottom, start on left and right edges
        for (int row = 0; row < _input.Length; row++)
        {
            energizedCounts.Add(CountEnergized(row, -1, 'E'));
            energizedCounts.Add(CountEnergized(row, _input.Length, 'W'));
        }

        // along the cols, left to right, start on top and bottom edges
        for (int col = 0; col < _input[0].Length; col++)
        {
            energizedCounts.Add(CountEnergized(0, col, 'S'));
            energizedCounts.Add(CountEnergized(_input[0].Length, col, 'N'));
        }

        return energizedCounts.Max();
    }

    private static int CountEnergized(int startRow, int startCol, char direction)
    {
        var start = new Photon(new Position(startCol, startRow), direction);
        var queue = new Queue<Photon>([start]);
        var visited = new HashSet<Photon>();

        var maxRow = _input.Length;
        var maxCol = _input[0].Length;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var nextPos = current.Position.MoveInDirection(current.Direction);

            if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X >= maxCol || nextPos.Y >= maxRow)
                continue;

            var nextDirections = GetNextDirections(_input[nextPos.Y][nextPos.X], current.Direction);
            foreach (var dir in nextDirections)
            {
                var nextPhoton = new Photon(nextPos, dir);
                if (visited.Add(nextPhoton))
                {
                    queue.Enqueue(nextPhoton);
                }
            }
        }

        return visited.DistinctBy(x => x.Position).Count();
    }

    private static char[] GetNextDirections(char tile, char direction)
        => (tile, direction) switch
        {
            ('.', _) or ('|', 'N') or ('|', 'S') or ('-', 'E') or ('-', 'W') => [direction],
            ('|', 'E') or ('|', 'W') => ['N', 'S'],
            ('-', 'N') or ('-', 'S') => ['E', 'W'],
            ('\\', 'N') or ('/', 'S')=> ['W'],
            ('\\', 'S') or ('/', 'N') => ['E'],
            ('\\', 'E') or ('/', 'W') => ['S'],
            ('\\', 'W') or ('/', 'E') => ['N'],
            _ => throw new NotImplementedException()
        };
}

public record Photon(Position Position, char Direction);