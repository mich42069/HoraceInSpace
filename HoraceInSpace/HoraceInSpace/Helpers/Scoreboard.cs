using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Holds the Score of all players, is created on first startup, and loaded every other startup from a .json file.
/// </summary>
public static class Scoreboard
{
    private const string FileName = "scoreboard.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private static readonly List<ScoreEntry> Scores = Load();

    /// <summary>
    /// Adds the score to the leaderboard. 
    /// </summary>
    /// <param name="name">Name of the record.</param>
    /// <param name="score">Score of the record.</param>
    public static void AddNewScore(string name, int score)
    {
        Scores.Add(new ScoreEntry(score, name));
        Scores.Sort((a, b) => b.Score.CompareTo(a.Score));
        Save();
    }

    /// <summary>
    /// Returns the top X records from the scoreboard.
    /// </summary>
    /// <param name="count">How many records from the scoreboard you want.</param>
    /// <returns>Read only list of top X records.</returns>
    public static IReadOnlyList<(int Score, string Name)> GetTopX(int count)
    {
        return Scores
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
        Save(Scores);
    }

    private static void Save(List<ScoreEntry> scores)
    {
        try
        {
            File.WriteAllText(
                FileName,
                JsonSerializer.Serialize(scores, JsonOptions));
        }
        catch
        {
            // If it fails to write the scoreboard we are not trying again,
            // since there isn't anything we can do about it.
        }
    }

    private sealed record ScoreEntry(int Score, string Name);
}