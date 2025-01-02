using Microsoft.Z3;

namespace Day24;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1()
    {
        var hailstones = GetHailstones();
        const long minBound = 200_000_000_000_000;
        const long maxBound = 400_000_000_000_000;

        return hailstones.CompareAll((h1, h2) => HailstoneFuturePathsCross(h1, h2, minBound, maxBound)).Count(x => x);
    }

    // Uses Z3 Solver: https://github.com/Z3Prover/z3
    private static long Part2()
    {
        var hailstones = GetHailstones();
        using var ctx = new Context();
        using var solver = ctx.MkSolver();

        // Stone position and velocity
        var (x, y, z) = (ctx.MkIntConst("x"), ctx.MkIntConst("y"), ctx.MkIntConst("z"));
        var (vx, vy, vz) = (ctx.MkIntConst("vx"), ctx.MkIntConst("vy"), ctx.MkIntConst("vz"));

        for (int i = 0; i < 3; i++)
        {
            var t = ctx.MkIntConst($"t{i}"); // time
            var hail = hailstones[i];

            var (px, py, pz) = (ctx.MkInt(hail.Position.X), ctx.MkInt(hail.Position.Y), ctx.MkInt(hail.Position.Z));
            var (pvx, pvy, pvz) = (ctx.MkInt(hail.Velocity.X), ctx.MkInt(hail.Velocity.Y), ctx.MkInt(hail.Velocity.Z));

            solver.Add(t >= 0); // time should always be positive
            solver.Add(ctx.MkEq(ctx.MkAdd(x, ctx.MkMul(t, vx)), ctx.MkAdd(px, ctx.MkMul(t, pvx)))); // x + t * vx = px + t * pvx
            solver.Add(ctx.MkEq(ctx.MkAdd(y, ctx.MkMul(t, vy)), ctx.MkAdd(py, ctx.MkMul(t, pvy)))); // y + t * vy = py + t * pvy
            solver.Add(ctx.MkEq(ctx.MkAdd(z, ctx.MkMul(t, vz)), ctx.MkAdd(pz, ctx.MkMul(t, pvz)))); // z + t * vz = pz + t * pvz
        }

        solver.Check();
        var model = solver.Model;

        var rockPosition = new[]
        {
            long.Parse(model.Eval(x).ToString()),
            long.Parse(model.Eval(y).ToString()),
            long.Parse(model.Eval(z).ToString())
        };

        return rockPosition.Sum();
    }

    private static bool HailstoneFuturePathsCross(Hailstone a, Hailstone b, long minBound, long maxBound)
    {
        if (a.Slope == b.Slope) return false;

        var x = (b.Intersect - a.Intersect) / (a.Slope - b.Slope);
        var y = ((b.Slope * a.Intersect) - (a.Slope * b.Intersect)) / (a.Slope - b.Slope) * -1;

        if (IsInPast(a, x, y) || IsInPast(b, x, y)) return false;

        return x.IsBetween(minBound, maxBound) && y.IsBetween(minBound, maxBound);
    }

    private static bool IsInPast(Hailstone hailstone, double x, double y)
        => x < hailstone.Position.X != hailstone.Velocity.X < 0
        || y < hailstone.Position.Y != hailstone.Velocity.Y < 0;

    private static Hailstone[] GetHailstones()
        => _input.Select(x => x.ExtractNumeric<long>().ToArray())
                 .Select(x => new Hailstone((x[0], x[1], x[2]), (x[3], x[4], x[5]))).ToArray();
}
