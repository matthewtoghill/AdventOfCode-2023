namespace Day04;

public class Program
{
    private static readonly int[] _matches = Input.ReadAllLines().Select(ParseCard).ToArray();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int ParseCard(string line)
    {
        var split = line.Split(["Card", ":", "|"], StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
        var winningNumbers = split[0].SplitTo<int>(" ");
        var cardNumbers = split[1].SplitTo<int>(" ");
        return cardNumbers.Intersect(winningNumbers).Count();
    }

    private static int Part1() => _matches.Sum(x => (int)Math.Pow(2, x - 1));

    private static int Part2()
    {
        var cards = new Dictionary<int, int>();
        for (int i = 0; i < _matches.Length; i++)
        {
            cards.IncrementAt(i);
            Enumerable.Range(1, _matches[i]).ForEach(j => cards.IncrementAt(i + j, cards[i]));
        }

        return cards.Values.Sum();
    }
}
