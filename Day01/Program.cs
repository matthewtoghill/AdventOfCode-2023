namespace Day01;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {_input.Sum(Part1)}");
        Console.WriteLine($"Part 2: {_input.Sum(Part2)}");
        Console.WriteLine($"Part 2 alt: {Part2Alt()}");
    }

    private static readonly Dictionary<string, int> _digitMap = new()
    {
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9
    };

    private static int Part1(string line)
        => int.Parse($"{line.First(char.IsNumber)}{line.Last(char.IsNumber)}");

    private static int Part2(string line)
    {
        List<int> digits = [];

        for (var i = 0; i < line.Length; i++)
        {
            if (char.IsNumber(line[i]))
            {
                digits.Add(line[i] - '0');
                continue;
            }

            foreach (var (key, digit) in _digitMap)
            {
                if (line.IndexOf(key, i) == i)
                {
                    digits.Add(digit);
                    i += key.Length - 2;
                    break;
                }
            }
        }

        return 10 * digits[0] + digits[^1];
    }

    // alternative solution to part 2 using a string replace strategy
    private static int Part2Alt()
        => Input.ReadAll()
                .Replace("one", "o1e")      //"one1one"
                .Replace("two", "t2o")      //"two2two"
                .Replace("three", "t3e")    //"three3three"
                .Replace("four", "f4r")     //"four4four"
                .Replace("five", "f5e")     //"five5five"
                .Replace("six", "s6x")      //"six6six"
                .Replace("seven", "s7n")    //"seven7seven"
                .Replace("eight", "e8t")    //"eight8eight"
                .Replace("nine", "n9e")     //"nine9nine"
                .SplitLines()
                .Sum(Part1);
}
