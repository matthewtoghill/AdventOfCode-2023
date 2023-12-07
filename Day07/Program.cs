namespace Day07;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve("23456789TJQKA", false)}");
        Console.WriteLine($"Part 2: {Solve("J23456789TQKA", true)}");
    }

    private static int Solve(string cardPriority, bool hasJokers)
        => _input.Select(x => ParseHand(x.Split(), cardPriority, hasJokers))
                 .Order()
                 .Select((hand, rank) => hand.Bid * (rank + 1))
                 .Sum();

    private static Hand ParseHand(string[] lineSplit, string cardPriority, bool hasJokers)
        => new (lineSplit[0], int.Parse(lineSplit[1]), cardPriority, hasJokers);
}
