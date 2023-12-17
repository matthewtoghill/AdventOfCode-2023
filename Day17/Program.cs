using AoC.Tools.Models;

namespace Day17;

public class Program
{
    private static readonly int[,] _input = Input.ReadAsGrid<int>();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1() => Solve([new State(new Position(0, 0), 'E', 0)], 1, 3);

    private static int Part2()
    {
        State[] startStates = [new State(new Position(0, 0), 'E', 0),
                               new State(new Position(0, 0), 'S', 0)];
        return Solve(startStates, 4, 10);
    }

    private static int Solve(State[] startStates, int minRunLength, int maxRunLength)
    {
        var end = new Position(_input.GetLength(1) - 1, _input.GetLength(0) - 1);
        var priorityQueue = new PriorityQueue<State, int>();
        var distances = new Dictionary<State, int>();

        foreach (var item in startStates)
        {
            priorityQueue.Enqueue(item, 0);
            distances[item] = 0;
        }

        while (priorityQueue.TryDequeue(out var current, out var currentCost))
        {
            if (current.Position == end && current.RunLength >= minRunLength)
                return currentCost;

            foreach (var direction in "NESW")
            {
                if (IsOppositeDirection(current.Direction, direction)) continue;
                if (current.RunLength < minRunLength && direction != current.Direction) continue;
                if (current.RunLength == maxRunLength && current.Direction == direction) continue;

                var nextPosition = current.Position.MoveInDirection(direction);
                if (nextPosition.X < 0 || nextPosition.Y < 0 || nextPosition.X > end.X || nextPosition.Y > end.Y)
                    continue;

                var runLength = direction == current.Direction ? current.RunLength + 1 : 1;
                var nextState = new State(nextPosition, direction, runLength);
                var cost = currentCost + _input[nextPosition.Y, nextPosition.X];

                if (!distances.TryGetValue(nextState, out int value) || cost < value)
                {
                    distances[nextState] = cost;
                    priorityQueue.Enqueue(nextState, cost);
                }
            }
        }

        return 0;
    }

    private static bool IsOppositeDirection(char currentDirection, char nextDirection)
        => (currentDirection, nextDirection) switch
        {
            ('N', 'S') or ('S', 'N') or ('E', 'W') or ('W', 'E') => true,
            _ => false
        };
}

public record State(Position Position, char Direction, int RunLength);