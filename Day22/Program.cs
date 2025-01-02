namespace Day22;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        var fallenBricks = GetFallingBrickCounts();
        Console.WriteLine($"Part 1: {fallenBricks.Count(x => x == 0)}");
        Console.WriteLine($"Part 2: {fallenBricks.Sum()}");
    }

    private static List<int> GetFallingBrickCounts()
    {
        var bricks = DropBricks(GetBricks().OrderBy(brick => brick.Bottom).ToArray());
        var supports = GetSupports(bricks);

        return bricks.AsParallel().Select(disintegratedBrick =>
        {
            Queue<Brick> queue = new([disintegratedBrick]);
            HashSet<Brick> falling = [];

            while (queue.TryDequeue(out var brick))
            {
                falling.Add(brick);

                supports.Above[brick]
                        .Where(x => supports.Below[x].IsSubsetOf(falling))
                        .ForEach(queue.Enqueue);
            }

            return falling.Count - 1;
        }).ToList();
    }

    private static Brick[] DropBricks(Brick[] bricks)
    {
        for (var i = 0; i < bricks.Length; i++)
        {
            var bottom = 1;
            for (var j = 0; j < i; j++)
            {
                if (IntersectsXY(bricks[i], bricks[j]))
                    bottom = Math.Max(bottom, bricks[j].Top + 1);
            }

            var distance = bricks[i].Z.Start - bottom;
            bricks[i] = bricks[i] with { Z = new(bricks[i].Bottom - distance, bricks[i].Top - distance) };
        }

        return bricks;
    }

    private static Supports GetSupports(Brick[] bricks)
    {
        var above = bricks.ToDictionary(b => b, _ => new HashSet<Brick>());
        var below = bricks.ToDictionary(b => b, _ => new HashSet<Brick>());

        for (var i = 0; i < bricks.Length; i++)
        {
            for (var j = i + 1; j < bricks.Length; j++)
            {
                if (bricks[j].Bottom == bricks[i].Top + 1 && IntersectsXY(bricks[i], bricks[j]))
                {
                    above[bricks[i]].Add(bricks[j]);
                    below[bricks[j]].Add(bricks[i]);
                }
            }
        }

        return new Supports(above, below);
    }

    private static Brick[] GetBricks()
        => _input.Select(line => line.ExtractInts().ToArray())
                 .Select((x, i) => new Brick(i, new(x[0], x[3]), new(x[1], x[4]), new(x[2], x[5]))).ToArray();

    private static bool IntersectsXY(Brick brickA, Brick brickB) => Intersects(brickA.X, brickB.X) && Intersects(brickA.Y, brickB.Y);
    private static bool Intersects(Range r1, Range r2) => r1.Start <= r2.End && r2.Start <= r1.End;
}

internal record Range(int Start, int End);
internal record Brick(int Id, Range X, Range Y, Range Z)
{
    public int Top => Z.End;
    public int Bottom => Z.Start;
};

internal record Supports(Dictionary<Brick, HashSet<Brick>> Above, Dictionary<Brick, HashSet<Brick>> Below);