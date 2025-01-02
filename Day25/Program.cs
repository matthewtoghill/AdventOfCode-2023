namespace Day25;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(3)}");
        Console.WriteLine($"Part 2: Happy Christmas!");
    }

    private static long Solve(int requiredCutSize)
    {
        var components = GetComponents();
        var iterations = 0;
        while (true)
        {
            iterations++;
            var graph = BuildGraph(components);

            while (graph.Count > 2)
                graph.Values.RandomElement().MergeRandomConnection(graph);

            if (graph.Values.All(x => x.Connections.Count == requiredCutSize))
                return graph.Values.Select(x => x.Value).Product();
        }
    }

    private static Dictionary<string, Component> BuildGraph(List<Component> components)
        => components.ToDictionary(k => k.Name, v => new Component(v.Name, v.Connections));

    private static List<Component> GetComponents()
    {
        List<Component> graph = [];

        void addRelationship(string name, string component)
        {
            var current = graph.FirstOrDefault(x => x.Name == name);
            if (current is null)
            {
                current = new Component(name);
                graph.Add(current);
            }
            current.Connections.Add(component);
        }

        foreach (var line in _input)
        {
            var split = line.Split([":", " "], StringSplitOptions.RemoveEmptyEntries);
            var name = split[0];
            var connections = split[1..];

            foreach (var wire in connections)
            {
                addRelationship(name, wire);
                addRelationship(wire, name);
            }
        }

        return graph;
    }
}
