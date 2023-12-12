using AoC.Tools.Models;

namespace Day10;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        var loop = GetLoop();
        Console.WriteLine($"Part 1: {loop.Count / 2}");
        Console.WriteLine($"Part 2: {Part2(loop)}");
    }

    private static List<Step> GetLoop()
    {
        var start = FindStart();

        foreach (var (startPipe, startDir) in "|-LJ7F|-LJ7F".Zip("NWNNWSSESSEN"))
        {
            var path = new List<Step>();
            var nextDir = GetNextDirection(startPipe, startDir);
            var current = new Step(start, startPipe, startDir);
            path.Add(current);

            while (true)
            {
                var nextPos = current.Pos.MoveInDirection(current.Direction);

                if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X >= _input[nextPos.Y].Length || nextPos.Y >= _input.Length)
                    continue;

                if (nextPos == start)
                    return path;

                if (!IsValid(current.Direction, nextPos))
                    break;

                var nextChar = _input[nextPos.Y][nextPos.X];
                nextDir = GetNextDirection(nextChar, current.Direction);

                current = new(nextPos, nextChar, nextDir);
                path.Add(current);
            }
        }

        return [];
    }

    private static Position FindStart()
    {
        for (int row = 0; row < _input.Length; row++)
            if (_input[row].Contains('S'))
                return new Position(_input[row].IndexOf('S'), row);

        return new Position(-1, -1);
    }

    private static char GetNextDirection(char pipe, char startDirection)
        => (pipe, startDirection) switch
        {
            ('|', 'N') or ('L', 'W') or ('J', 'E') => 'N',
            ('|', 'S') or ('7', 'E') or ('F', 'W') => 'S',
            ('-', 'E') or ('L', 'S') or ('F', 'N') => 'E',
            ('-', 'W') or ('J', 'S') or ('7', 'N') => 'W',
            _ => '?'
        };

    private static bool IsValid(char direction, Position next)
        => (direction, _input[next.Y][next.X]) switch
        {
            ('N', '7') or ('N', 'F') or ('N', '|') => true,
            ('S', 'L') or ('S', 'J') or ('S', '|') => true,
            ('E', 'J') or ('E', '7') or ('E', '-') => true,
            ('W', 'L') or ('W', 'F') or ('W', '-') => true,
            _ => false
        };

    private static int Part2(List<Step> loop)
    {
        var allPos = loop.Select(x => x.Pos).ToList();
        allPos.Add(loop[0].Pos);

        return allPos.CountPositionsInsideArea();
    }
}

public record Step(Position Pos, char Pipe, char Direction);
