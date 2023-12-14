using System.Text;
using AoC.Tools.Models;

namespace Day14;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(1, "N")}");
        Console.WriteLine($"Part 2: {Solve(1_000_000_000, "NWSE")}");
    }
    private static int Solve(int maxCycles, string directions)
    {
        var map = LoadMap();
        var maxRow = map.Keys.MaxBy(x => x.Y).Y;
        var maxCol = map.Keys.MaxBy(x => x.X).X;

        var cycle = 1;
        Dictionary<string, int> cache = [];

        while (cycle <= maxCycles)
        {
            foreach (var direction in directions)
            {
                TiltMap(map, direction, maxRow, maxCol);
            }

            string hash = GetMapHash(map);

            if (cache.TryGetValue(hash, out var value))
            {
                cycle = maxCycles - ((maxCycles - cycle) % (cycle - value));
                cache.Clear();
            }

            cache[hash] = cycle++;
        }

        return CalculateLoad(map, maxRow);
    }

    private static void TiltMap(Dictionary<Position, char> map, char direction, int maxRow, int maxCol)
    {
        if ("SE".Contains(direction))
        {
            foreach (var (pos, rock) in map.Reverse())
            {
                SlideRock(map, direction, maxRow, maxCol, pos, rock);
            }
        }
        else
        {
            foreach (var (pos, rock) in map)
            {
                SlideRock(map, direction, maxRow, maxCol, pos, rock);
            }
        }
    }

    private static void SlideRock(Dictionary<Position, char> map, char direction, int maxRow, int maxCol, Position pos, char rock)
    {
        if (rock != 'O') return;

        var currentPos = pos;
        while (true)
        {
            var newPos = currentPos.MoveInDirection(direction);
            if (direction == 'N' && newPos.Y < 0) return;
            if (direction == 'W' && newPos.X < 0) return;
            if (direction == 'S' && newPos.Y > maxRow) return;
            if (direction == 'E' && newPos.X > maxCol) return;
            if (map.TryGetValue(newPos, out var nextRock) && "O#".Contains(nextRock)) return;

            map[newPos] = 'O';
            map[currentPos] = '.';
            currentPos = newPos;
        }
    }

    private static string GetMapHash(Dictionary<Position, char> rocks)
    {
        var sb = new StringBuilder();
        rocks.Values.ForEach(x => sb.Append(x));
        return sb.ToString();
    }

    private static int CalculateLoad(Dictionary<Position, char> rocks, int maxRow)
    {
        var result = 0;
        var roundRocks = rocks.Where(x => x.Value == 'O').Select(x => x.Key.Y).ToList();
        roundRocks.ForEach(x => result += maxRow + 1 - x);
        return result;
    }

    private static Dictionary<Position, char> LoadMap()
    {
        Dictionary<Position, char> rocks = [];
        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col < _input[row].Length; col++)
            {
                rocks.Add(new Position(col, row), _input[row][col]);
            }
        }

        return rocks;
    }
}