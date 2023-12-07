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
    {
        var hands = _input.Select(x => ParseHand(x, cardPriority, hasJokers)).OrderDescending().ToList();
        return hands.Select((x, i) => x.Bid * (i + 1)).Sum();
    }

    private static Hand ParseHand(string line, string cardPriority, bool hasJokers)
    {
        var split = line.Split();
        return new Hand(split[0], int.Parse(split[1]), cardPriority, hasJokers);
    }
}

public class Hand : IComparable<Hand>
{
    public string CardPriority { get; init; }
    public string Cards { get; init; }
    public int Bid { get; init; }
    public Dictionary<char, int> CardFrequencies { get; private set; }
    public int HandScore { get; private set; }

    public Hand(string cards, int bid, string cardPriority, bool hasJokers)
    {
        Cards = cards;
        Bid = bid;
        CardPriority = cardPriority;
        CardFrequencies = cards.GetFrequencies();

        if (hasJokers && CardFrequencies.ContainsKey('J') && CardFrequencies.Count != 1)
        {
            var mostFrequent = CardFrequencies.Where(x => x.Key != 'J').MaxBy(x => x.Value).Key;
            var updatedCards = cards.Replace('J', mostFrequent);
            CardFrequencies = updatedCards.GetFrequencies();
        }

        HandScore = GetHandScore();
    }

    public int CompareTo(Hand? other)
    {
        if (other is null) return 1;

        if (HandScore > other.HandScore) return -1;
        if (HandScore < other.HandScore) return 1;

        for (int i = 0; i < Cards.Length; i++)
        {
            if (CardPriority.IndexOf(Cards[i]) > CardPriority.IndexOf(other.Cards[i])) return -1;
            if (CardPriority.IndexOf(Cards[i]) < CardPriority.IndexOf(other.Cards[i])) return 1;
        }

        return 0;
    }

    private int GetHandScore()
    {
        if (IsFiveOfKind()) return 6;
        if (IsFourOfKind()) return 5;
        if (IsFullHouse()) return 4;
        if (IsThreeOfKind()) return 3;
        if (IsTwoPair()) return 2;
        if (IsPair()) return 1;
        return 0;
    }

    private bool IsFiveOfKind() => CardFrequencies.Count == 1;
    private bool IsFourOfKind() => CardFrequencies.ContainsValue(4);
    private bool IsFullHouse() => CardFrequencies.Count == 2 && CardFrequencies.ContainsValue(3);
    private bool IsThreeOfKind() => CardFrequencies.Count == 3 && CardFrequencies.ContainsValue(3);
    private bool IsTwoPair() => CardFrequencies.Count == 3 && CardFrequencies.Values.Where(x => x == 2).Count() == 2;
    private bool IsPair() => CardFrequencies.Count == 4 && CardFrequencies.ContainsValue(2);
}
