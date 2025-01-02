namespace Day25;

public record Component(string Name)
{
    public List<string> Connections { get; set; } = [];
    public int Value { get; private set; } = 1;

    public Component(string name, IEnumerable<string> connections) : this(name)
    {
        Connections = connections.ToList();
    }

    public void MergeRandomConnection(Dictionary<string, Component> components)
    {
        var component = components[Connections.RandomElement()];
        Value += component.Value;
        Connections.AddRange(component.Connections);

        foreach (var item in component.Connections)
        {
            while (components[item].Connections.Remove(component.Name))
                components[item].Connections.Add(Name);
        }

        Connections.RemoveAll(x => x == Name);
        components.Remove(component.Name);
    }
}