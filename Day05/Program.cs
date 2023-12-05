namespace Day05;

public class Program
{
    private static readonly string[] _input = Input.ReadAsParagraphs().ToArray();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static long Part1() => GetLowestLocation(_input[0].ExtractNumeric<long>().ToArray(), GetMaps());

    private static long Part2()
    {
        var seeds = _input[0].ExtractNumeric<long>().Chunk(2).Select(x => new SeedRange(x[0], x[0] + x[1])).ToArray();
        var maps = GetMaps();
        var candidateSeeds = new List<long>();

        for (int i = 0; i < maps.Count; i++)
        {
            var map = maps[i];
            foreach (var item in map)
            {
                var current = item.SourceStart;
                for (int j = i - 1; j >= 0; j--)
                {
                    current = ApplyReverseMap(current, maps[j]);
                }
                candidateSeeds.Add(current);
            }
        }

        return GetLowestLocation(candidateSeeds.Where(x => seeds.Any(s => x.IsBetween(s.Start, s.End))).ToArray(), maps);
    }

    private static long GetLowestLocation(long[] seeds, List<CategoryMap[]> maps)
    {
        var lowestLocation = long.MaxValue;

        foreach (var seed in seeds)
        {
            var current = seed;
            maps.ForEach(x => current = ApplyMap(current, x));
            lowestLocation = Math.Min(current, lowestLocation);
        }

        return lowestLocation;
    }

    private static List<CategoryMap[]> GetMaps() => _input.Skip(1).Select(GetMap).ToList();

    private static CategoryMap[] GetMap(string text)
        => text.SplitLines()
               .Skip(1)
               .Select(x => x.ExtractNumeric<long>().ToArray())
               .Select(x => new CategoryMap(x[0], x[1], x[2], x[0] - x[1]))
               .ToArray();

    private static long ApplyMap(long source, CategoryMap[] map)
    {
        foreach (var item in map)
        {
            if (source >= item.SourceStart && source < item.SourceStart + item.Length)
                return source + item.Difference;
        }

        return source;
    }

    private static long ApplyReverseMap(long destination, CategoryMap[] map)
    {
        foreach (var item in map)
        {
            if (destination >= item.DestinationStart && destination < item.DestinationStart + item.Length)
                return destination - item.Difference;
        }
        return destination;
    }
}

public record CategoryMap(long DestinationStart, long SourceStart, long Length, long Difference);

public record SeedRange(long Start, long End);
