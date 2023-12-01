﻿namespace AoC.Tools;

public static class Input
{
    public static string ReadAll()
        => File.ReadAllText(@$"..\..\..\..\data\{System.Reflection.Assembly.GetEntryAssembly()!.GetName().Name}.txt");

    public static string ReadAll(int day)
        => File.ReadAllText(@$"..\..\..\..\data\day{day:00}.txt");

    public static string[] ReadAllLines()
        => File.ReadAllLines(@$"..\..\..\..\data\{System.Reflection.Assembly.GetEntryAssembly()!.GetName().Name}.txt");

    public static string[] ReadAllLines(int day)
        => File.ReadAllLines(@$"..\..\..\..\data\day{day:00}.txt");

    public static IEnumerable<T> ReadAllLinesTo<T>() where T : IParsable<T>
        => ReadAll().Split(["\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => T.Parse(x, null));

    public static IEnumerable<T> ReadAllLinesTo<T>(int day) where T : IParsable<T>
        => ReadAll(day).Split(["\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries)
                       .Select(x => T.Parse(x, null));

    public static IEnumerable<T> SplitTo<T>(this string text) where T : IParsable<T>
        => text.Split(["\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries)
               .Select(x => T.Parse(x, null));

    public static IEnumerable<T> SplitTo<T>(this string text, params string[] separators) where T : IParsable<T>
        => text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
               .Select(x => T.Parse(x, null));

    public static string[] SplitOn(this string text, StringSplitOptions splitOptions, params string[] separators)
        => text.Split(separators, splitOptions);

    public static string[] SplitOn(this string text, params string[] separators)
        => text.Split(separators, StringSplitOptions.None);

    public static string[] SplitOn(this string text, StringSplitOptions splitOptions, params char[] separators)
        => text.Split(separators, splitOptions);

    public static string[] SplitOn(this string text, params char[] separators)
        => text.Split(separators, StringSplitOptions.None);

    public static IEnumerable<string> ReadAsParagraphs()
        => ReadAll().Split(["\n\n", "\r\n\r\n"], StringSplitOptions.RemoveEmptyEntries);

    public static IEnumerable<string> ReadAsParagraphs(int day)
        => ReadAll(day).Split(["\n\n", "\r\n\r\n"], StringSplitOptions.RemoveEmptyEntries);

    public static T[,] ReadAsGrid<T>() where T : IParsable<T>
    {
        var lines = ReadAllLines();
        var grid = new T[lines.Length, lines[0].Length];
        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines[x].Length; y++)
            {
                grid[x, y] = T.Parse(lines[x][y].ToString(), null);
            }
        }
        return grid;
    }
}
