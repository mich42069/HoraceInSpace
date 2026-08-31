using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Holds the scores of all players. The scoreboard is stored in the user's
/// AppData folder and persists independently of the game installation files.
/// </summary>
public static class Scoreboard
{
    private const string AppFolderName = "HoraceInSpace";
    private const string FileName = "scoreboard.json";

    private static readonly string DirectoryPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        AppFolderName);

    private static readonly string FilePath = Path.Combine(
        DirectoryPath,
        FileName);

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
    /// <returns>Read-only list of the top X records.</returns>
    public static IReadOnlyList<(int Score, string Name)> GetTopX(int count)
    {
        return Scores
            .Take(count)
            .Select(s => (s.Score, s.Name))
            .ToList();
    }

    private static List<ScoreEntry> Load()
    {
        try
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            if (!File.Exists(FilePath))
            {
                Save([]);
                return [];
            }

            return JsonSerializer.Deserialize<List<ScoreEntry>>(
                       File.ReadAllText(FilePath))
                   ?? [];
        }
        catch
        {
            // If the file cannot be read or is corrupt,
            // start with an empty scoreboard.
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
            Directory.CreateDirectory(DirectoryPath);

            File.WriteAllText(
                FilePath,
                JsonSerializer.Serialize(scores, JsonOptions));
        }
        catch
        {
            // If saving fails, there is nothing useful we can do here.
        }
    }

    private sealed record ScoreEntry(int Score, string Name);
}