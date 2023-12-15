namespace Day15;

public class Program
{
    private static readonly string[] _input = Input.ReadAll().TrimEnd('\n').Split(',');
    static void Main()
    {
        Console.WriteLine($"Part 1: {_input.Sum(CalculateHash)} = 515495");
        Console.WriteLine($"Part 2: {Part2()} = 229349");
    }

    private static int CalculateHash(string text) => text.Aggregate(0, (c, total) => (total + c) * 17 % 256);

    private static int Part2()
    {
        var boxes = new List<List<Lens>>();
        Enumerable.Range(0, 256).ForEach(x => boxes.Add([]));

        foreach (var item in _input)
        {
            var split = item.Split(["=", "-"], StringSplitOptions.RemoveEmptyEntries);
            var label = split[0];
            var focalLength = split.Length == 1 ? -1 : int.Parse(split[1]);

            var box = CalculateHash(label);

            var lensIndex = boxes[box].FindIndex(x => x.Label == label);

            switch (split.Length, lensIndex)
            {
                case (1, >= 0): boxes[box].RemoveAt(lensIndex); break;
                case (2,   -1): boxes[box].Add(new(label, focalLength)); break;
                case (2, >= 0): boxes[box][lensIndex] = new(label, focalLength); break;
            };
        }

        var result = 0;

        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                result += (i + 1) * boxes[i][j].FocalLength * (j + 1);
            }
        }

        return result;
    }

}

public record Lens(string Label, int FocalLength);