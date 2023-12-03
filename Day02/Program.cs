﻿namespace Day02;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    private static readonly (int red, int green, int blue) _bagMax = (12, 13, 14);
    static void Main()
    {
        var (part1, part2) = BothParts();
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    private static (int part1, int part2) BothParts()
    {
        var validGames = new List<int>();
        var gamePowers = new List<int>();

        foreach (var line in _input)
        {
            var gameId = int.Parse(line.Split(["Game ", ":"], StringSplitOptions.RemoveEmptyEntries)[0]);
            var cubes = line.Split(["Game", ":", ";", ",", " "], StringSplitOptions.RemoveEmptyEntries).Skip(1).Chunk(2).ToArray();
            bool isImpossible = false;
            int redMax = 0, greenMax = 0, blueMax = 0;

            foreach(var cube in cubes)
            {
                var num = int.Parse(cube[0]);
                var colour = cube[1];

                if (colour == "red")
                {
                    if (num > _bagMax.red) isImpossible = true;
                    redMax = Math.Max(redMax, num);
                }
                else if (colour == "green")
                {
                    if (num > _bagMax.green) isImpossible = true;
                    greenMax = Math.Max(greenMax, num);
                }
                else if (colour == "blue")
                {
                    if (num > _bagMax.blue) isImpossible = true;
                    blueMax = Math.Max(blueMax, num);
                }
            }

            gamePowers.Add(redMax * greenMax * blueMax);
            if (!isImpossible) validGames.Add(gameId);
        }

        return (validGames.Sum(), gamePowers.Sum());
    }
}
