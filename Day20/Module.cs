namespace Day20;

public enum ModuleType
{
    None,
    Conjunction,
    FlipFlop
}

public record Module(string Id, ModuleType Type)
{
    public bool IsHighPulse { get; set; } = false;
    public bool IsOn { get; set; } = false;
    public HashSet<string> DestinationIds { get; set; } = [];
    public Dictionary<string, bool> ConjunctionPulseMemory = [];

    public Module(string id, ModuleType type, HashSet<string> destinationIds) : this(id, type)
    {
        DestinationIds = destinationIds;
    }
}