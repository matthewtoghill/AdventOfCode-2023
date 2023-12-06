namespace Day06;

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
        var times = _input[0].ExtractNumeric<long>().ToArray();
        var distances = _input[1].ExtractNumeric<long>().ToArray();

        return times.Select((time, i) => CalculateWinningRaces(time, distances[i])).Product();
    }

    private static long Part2()
    {
        long maxTime = _input[0].Replace(" ", "").ExtractNumeric<long>().First();
        long distance = _input[1].Replace(" ", "").ExtractNumeric<long>().First();

        return CalculateWinningRaces(maxTime, distance);
    }

    private static long CalculateWinningRaces(long maxTime, long distance)
    {
        for (long speed = 1; speed < (maxTime / 2); speed++)
        {
            if (speed * (maxTime - speed) > distance)
            {
                return maxTime - (speed * 2) + 1;
            }
        }

        return 0;
    }
}
