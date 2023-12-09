namespace Day09;

public class Program
{
    private static readonly List<List<int>> _input = Input.ReadAllLines().Select(x => x.ExtractNumeric<int>().ToList()).ToList();
    static void Main()
    {
        var (part1, part2) = Solve();
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    private static (int, int) Solve()
    {
        List<int> firstNumbers = [];
        List<int> lastNumbers = [];

        foreach (var line in _input)
        {
            List<List<int>> gapSequences = [];
            gapSequences.Add(line);
            var current = line;
            while (true)
            {
                var differences = current.EnumerateDifferences().ToList();

                gapSequences.Add(differences);
                if (differences.All(x => x == 0)) break;
                current = differences;
            }

            for (int i = gapSequences.Count - 2; i >= 0; i--)
            {
                gapSequences[i].Add(gapSequences[i][^1] + gapSequences[i + 1][^1]);
                gapSequences[i].Insert(0, gapSequences[i][0] - gapSequences[i + 1][0]);
            }

            firstNumbers.Add(gapSequences[0][0]);
            lastNumbers.Add(gapSequences[0][^1]);
        }

        return (lastNumbers.Sum(), firstNumbers.Sum());
    }
}
