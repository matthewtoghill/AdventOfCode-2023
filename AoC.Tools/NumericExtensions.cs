namespace AoC.Tools;

public static class NumericExtensions
{
    public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
    {
        return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
    }

    public static int ManhattanDistance(this (int X, int Y) startPos, (int X, int Y) endPos)
    {
        return Math.Abs(startPos.X - endPos.X) + Math.Abs(startPos.Y - endPos.Y);
    }

    public static double DirectDistance(this (int X, int Y) startPos, (int X, int Y) endPos)
    {
        return Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));
    }

    public static int ChessDistance(this (int X, int Y) startPos, (int X, int Y) endPos)
    {
        return Math.Max(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
    }

    public static bool IsWithinBounds(this (int X, int Y) pos, (int X, int Y) minPos, (int X, int Y) maxPos)
    {
        return minPos.X <= pos.X && pos.X <= maxPos.X
            && minPos.Y <= pos.Y && pos.Y <= maxPos.Y;
    }

    public static bool IsOutsideBounds(this (int X, int Y) pos, (int X, int Y) minPos, (int X, int Y) maxPos)
    {
        return minPos.X > pos.X || pos.X > maxPos.X
            || minPos.Y > pos.Y || pos.Y > maxPos.Y;
    }


    public static long GreatestCommonFactor(int x, int y)
    {
        var b = Math.Max(x, y);
        var r = Math.Min(x, y);

        while (r != 0)
        {
            var a = b;
            b = r;
            r = a % b;
        }

        return b;
    }

    public static long GreatestCommonFactor(long x, long y)
    {
        var b = Math.Max(x, y);
        var r = Math.Min(x, y);

        while (r != 0)
        {
            var a = b;
            b = r;
            r = a % b;
        }

        return b;
    }

    public static long LowestCommonMultiple(int x, int y) => x / GreatestCommonFactor(x, y) * y;
    public static long LowestCommonMultiple(long x, long y) => x / GreatestCommonFactor(x, y) * y;

    public static long LowestCommonMultiple(this IEnumerable<int> items)
    {
        var uniqueValues = new HashSet<int>(items).ToArray();

        if (uniqueValues.Length == 0) return 0;
        if (uniqueValues.Length == 1) return uniqueValues[0];

        var current = LowestCommonMultiple(uniqueValues[0], uniqueValues[1]);

        for (int i = 2; i < uniqueValues.Length; i++)
        {
            current = LowestCommonMultiple(current, uniqueValues[i]);
        }

        return current;
    }
}
