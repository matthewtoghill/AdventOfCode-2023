using AoC.Tools.Models;

namespace Day23;

using Graph = Dictionary<Position, List<State>>;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    private static readonly Position MinPos = new(0, 0);
    private static readonly Position MaxPos = new(_input.Length - 1, _input[0].Length - 1);
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(true)}");
        Console.WriteLine($"Part 2: {Solve(false)}");
    }

    private static int Solve(bool includeSlopes)
    {
        var start = new Position(_input[0].IndexOf('.'), 0);
        var end = new Position(_input[^1].IndexOf('.'), _input.Length - 1);

        var crossroads = GetCrossroads(start, end).ToHashSet();
        var graph = BuildGraph(crossroads, includeSlopes);

        return FindLongestPath(graph, [], new State(start, 0), end);
    }

    private static Graph BuildGraph(HashSet<Position> crossroads, bool includeSlopes)
    {
        var graph = new Graph();

        foreach (var start in crossroads)
        {
            graph[start] = [];

            foreach (var end in crossroads.Where(c => c != start))
            {
                if (TryGetDistance(crossroads, start, end, includeSlopes, out int distance))
                    graph[start].Add(new State(end, distance));
            }
        }

        return graph;
    }

    private static int FindLongestPath(Graph graph, HashSet<Position> visited, State current, Position end)
    {
        if (current.Position == end)
            return current.Distance;

        var maxLength = 0;

        foreach (var next in graph[current.Position])
        {
            if (!visited.Add(next.Position)) continue;
            var length = FindLongestPath(graph, visited, new(next.Position, current.Distance + next.Distance), end);
            maxLength = Math.Max(maxLength, length);

            visited.Remove(next.Position);
        }

        return maxLength;
    }

    private static bool TryGetDistance(HashSet<Position> crossroads, Position start, Position end, bool includeSlopes, out int distance)
    {
        Queue<(Position Position, int Steps)> queue = new([(start, 0)]);
        HashSet<Position> visited = [];
        distance = 0;

        while (queue.TryDequeue(out var current))
        {
            if (!visited.Add(current.Position)) continue;

            if (current.Position == end)
            {
                distance = current.Steps;
                return true;
            }

            if (crossroads.Contains(current.Position) && current.Position != start) continue;

            foreach (var neighbour in GetValidNeighbours(current.Position, includeSlopes))
            {
                queue.Enqueue((neighbour, current.Steps + 1));
            }
        }

        return false;
    }

    private static IEnumerable<Position> GetValidNeighbours(Position position, bool includeSlopes)
    {
        foreach (var neighbour in position.GetNeighbours())
        {
            if (neighbour.IsOutsideBounds(MinPos, MaxPos)) continue;

            char c = _input[neighbour.Row][neighbour.Col];
            if (c == '#') continue;

            if (includeSlopes && !IsValidSlope(position, neighbour, c)) continue;

            yield return neighbour;
        }
    }

    private static bool IsValidSlope(Position current, Position neighbour, char slope)
    {
        if (!">v<^".Contains(slope)) return true;

        var direction = neighbour - current;
        return slope switch
        {
            '>' => direction == (1, 0),
            '<' => direction == (-1, 0),
            'v' => direction == (0, 1),
            '^' => direction == (0, -1),
            _ => true
        };
    }

    private static IEnumerable<Position> GetCrossroads(Position start, Position end)
    {
        yield return start;

        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col < _input[row].Length; col++)
            {
                if (_input[row][col] != '.') continue;

                var current = new Position(col, row);
                int wallCount = current.GetNeighbours().Count(n => n.IsBetween(MinPos, MaxPos) && _input[n.Row][n.Col] == '#');

                if (wallCount < 2)
                    yield return current;
            }
        }

        yield return end;
    }
}

internal record State(Position Position, int Distance);
