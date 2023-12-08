namespace Day08;

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
        var (instructions, nodes) = ParseInput();

        var current = "AAA";
        var steps = 0;

        while (current != "ZZZ")
        {
            var node = nodes[current];
            switch (instructions[steps % instructions.Length])
            {
                case 'L': current = node.Left; break;
                case 'R': current = node.Right; break;
            }

            steps++;
        }

        return steps;
    }

    private static long Part2()
    {
        var (instructions, nodes) = ParseInput();
        var startingNodes = nodes.Keys.Where(x => x.EndsWith('A')).ToArray();
        var stepList = new List<int>();

        foreach (var start in startingNodes)
        {
            var current = start;
            var steps = 0;

            while (!current.EndsWith('Z'))
            {
                var node = nodes[current];
                switch (instructions[steps % instructions.Length])
                {
                    case 'L': current = node.Left; break;
                    case 'R': current = node.Right; break;
                }

                steps++;
            }

            stepList.Add(steps);
        }

        return stepList.LowestCommonMultiple();
    }

    private static (string, Dictionary<string, Node>) ParseInput()
    {
        Dictionary<string, Node> nodes = [];
        foreach (var item in _input.Skip(2))
        {
            var split = item.Split(["=", "(", ",", ")", " "], StringSplitOptions.RemoveEmptyEntries);
            var start = split[0];
            nodes.Add(split[0], new Node(split[1], split[2]));
        }

        return (_input[0], nodes);
    }
}

public record Node(string Left, string Right);
