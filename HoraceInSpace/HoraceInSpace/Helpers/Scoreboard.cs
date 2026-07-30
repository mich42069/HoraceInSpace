using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HoraceInSpace.Helpers;

public static class Scoreboard
{
    private const string FileName = "scoreboard.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private static readonly List<ScoreEntry> _scores = Load();

    public static void AddNewScore(string name, int score)
    {
        _scores.Add(new ScoreEntry(score, name));
        _scores.Sort((a, b) => b.Score.CompareTo(a.Score));
        Save();
    }

    public static IReadOnlyList<(int Score, string Name)> GetTopX(int count)
    {
        return _scores
            .Take(count)
            .Select(s => (s.Score, s.Name))
            .ToList();
    }

    private static List<ScoreEntry> Load()
    {
        if (!File.Exists(FileName))
        {
            Save([]);
            return [];
        }

        try
        {
            return JsonSerializer.Deserialize<List<ScoreEntry>>(
                       File.ReadAllText(FileName))
                   ?? [];
        }
        catch
        {
            // If the file is corrupt, start with an empty scoreboard.
            return [];
        }
    }

    private static void Save()
    {
        Save(_scores);
    }

    private static void Save(List<ScoreEntry> scores)
    {
        File.WriteAllText(
            FileName,
            JsonSerializer.Serialize(scores, JsonOptions));
    }

    private sealed record ScoreEntry(int Score, string Name);
}