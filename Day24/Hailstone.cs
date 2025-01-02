namespace Day24;

public class Hailstone
{
    public double Slope { get; }
    public double Intersect { get; }
    public (long X, long Y, long Z) Position { get; }
    public (long X, long Y, long Z) Velocity { get; }

    public Hailstone((long X, long Y, long Z) position, (long X, long Y, long Z) velocity)
    {
        Position = position;
        Velocity = velocity;

        (long futureX, long futureY) = (Position.X + Velocity.X, Position.Y + Velocity.Y);
        Slope = (futureY - Position.Y) / (double)(futureX - Position.X);
        Intersect = futureY - (Slope * futureX);
    }
};
