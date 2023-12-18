using AoC.Tools.Models;

namespace AoC.Tools;

public static class PointExtensions
{
    public static int ManhattanDistance(this (int X, int Y) startPos, (int X, int Y) endPos) => Math.Abs(startPos.X - endPos.X) + Math.Abs(startPos.Y - endPos.Y);
    public static long ManhattanDistance(this (long X, long Y) startPos, (long X, long Y) endPos) => Math.Abs(startPos.X - endPos.X) + Math.Abs(startPos.Y - endPos.Y);

    public static double DirectDistance(this (int X, int Y) startPos, (int X, int Y) endPos) => Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));
    public static double DirectDistance(this (long X, long Y) startPos, (long X, long Y) endPos) => Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));

    public static int ChessDistance(this (int X, int Y) startPos, (int X, int Y) endPos) => Math.Max(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
    public static long ChessDistance(this (long X, long Y) startPos, (long X, long Y) endPos) => Math.Max(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));

    public static bool IsWithinBounds(this (int X, int Y) pos, (int X, int Y) minPos, (int X, int Y) maxPos)
    {
        return minPos.X <= pos.X && pos.X <= maxPos.X
            && minPos.Y <= pos.Y && pos.Y <= maxPos.Y;
    }

    public static bool IsWithinBounds(this (long X, long Y) pos, (long X, long Y) minPos, (long X, long Y) maxPos)
    {
        return minPos.X <= pos.X && pos.X <= maxPos.X
            && minPos.Y <= pos.Y && pos.Y <= maxPos.Y;
    }

    public static bool IsOutsideBounds(this (int X, int Y) pos, (int X, int Y) minPos, (int X, int Y) maxPos)
    {
        return minPos.X > pos.X || pos.X > maxPos.X
            || minPos.Y > pos.Y || pos.Y > maxPos.Y;
    }

    public static bool IsOutsideBounds(this (long X, long Y) pos, (long X, long Y) minPos, (long X, long Y) maxPos)
    {
        return minPos.X > pos.X || pos.X > maxPos.X
            || minPos.Y > pos.Y || pos.Y > maxPos.Y;
    }

    public static (int, int) MoveInDirection(this (int X, int Y) pos, char direction, int distance = 1)
    {
        return char.ToLower(direction) switch
        {
            'u' or '^' or 'n' => (pos.X, pos.Y - distance),
            'd' or 'v' or 's' => (pos.X, pos.Y + distance),
            'l' or '<' or 'w' => (pos.X - distance, pos.Y),
            'r' or '>' or 'e' => (pos.X + distance, pos.Y),
            _ => (pos.X, pos.Y),
        };
    }

    public static (long, long) MoveInDirection(this (long X, long Y) pos, char direction, long distance = 1)
    {
        return char.ToLower(direction) switch
        {
            'u' or '^' or 'n' => (pos.X, pos.Y - distance),
            'd' or 'v' or 's' => (pos.X, pos.Y + distance),
            'l' or '<' or 'w' => (pos.X - distance, pos.Y),
            'r' or '>' or 'e' => (pos.X + distance, pos.Y),
            _ => (pos.X, pos.Y),
        };
    }

    public static int CalculateArea(this List<(int X, int Y)> positions)
    {
        var result = 0;
        var zip = positions.Zip(positions.Skip(1));
        foreach (var (a, b) in zip)
        {
            result += (a.X * b.Y) - (a.Y * b.X);
        }

        return Math.Abs(result);
    }

    public static long CalculateArea(this List<(long X, long Y)> positions)
    {
        long result = 0;
        var zip = positions.Zip(positions.Skip(1));
        foreach (var (a, b) in zip)
        {
            result += (a.X * b.Y) - (a.Y * b.X);
        }

        return Math.Abs(result);
    }
}
