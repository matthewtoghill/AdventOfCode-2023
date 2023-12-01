namespace Day01;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {_input.Sum(Part1)}");
        Console.WriteLine($"Part 2: {_input.Sum(Part2)}");
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
}
