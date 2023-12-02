namespace Day02;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        var (part1, part2) = BothParts(12, 13, 14);
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    private static (int part1, int part2) BothParts(int red, int green, int blue)
    {
        var validGames = new List<int>();
        var gamePowers = new List<int>();

        foreach (var line in _input)
        {
            var gameId = int.Parse(line.SplitOn(StringSplitOptions.RemoveEmptyEntries, "Game ", ":")[0]);
            var rounds = line.SplitOn(StringSplitOptions.RemoveEmptyEntries, "Game", ":", ";").Skip(1).ToArray();
            bool isImpossible = false;
            int redMin = 0, greenMin = 0, blueMin = 0;

            foreach (var round in rounds)
            {
                var cubes = round.SplitOn(StringSplitOptions.RemoveEmptyEntries, ",", " ").Chunk(2).ToArray();

                foreach(var cube in cubes)
                {
                    var num = int.Parse(cube[0]);
                    var colour = cube[1];

                    if (colour == "red")
                    {
                        if (num > red) isImpossible = true;
                        if (num > redMin) redMin = num;
                    }
                    else if (colour == "green")
                    {
                        if (num > green) isImpossible = true;
                        if (num > greenMin) greenMin = num;
                    }
                    else if (colour == "blue")
                    {
                        if (num > blue) isImpossible = true;
                        if (num > blueMin) blueMin = num;
                    }
                }
            }

            gamePowers.Add(redMin * greenMin * blueMin);
            if (!isImpossible) validGames.Add(gameId);
        }

        return (validGames.Sum(), gamePowers.Sum());
    }
}
