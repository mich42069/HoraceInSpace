using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public static class Scoreboard
{
    private static List<(int, string)> _keptScores;

    private const string FileName = "scoreboard.json";

    static Scoreboard()
    {
        Initialize();
    }

    private static void Initialize()
    {
        if (!File.Exists(FileName))
        {
            _keptScores = new List<(int, string)>();
            Save();
            return;
        }

        string json = File.ReadAllText(FileName);

        List<ScoreEntry>? scores = JsonSerializer.Deserialize<List<ScoreEntry>>(json);

        _keptScores = scores?
                          .Select(s => (s.Score, s.Name))
                          .ToList()
                      ?? new List<(int, string)>();
    }

    public static void AddNewScore(string name, int score)
    {
        _keptScores.Add((score, name));
        Save();
    }

    public static List<(int, string)> GetTopX(int x)
    {
        return _keptScores
            .OrderByDescending(s => s.Item1)
            .Take(x)
            .ToList();
    }

    private static void Save()
    {
        List<ScoreEntry> scores = _keptScores
            .Select(s => new ScoreEntry
            {
                Score = s.Item1,
                Name = s.Item2
            })
            .ToList();

        string json = JsonSerializer.Serialize(
            scores,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(FileName, json);
    }

    private class ScoreEntry
    {
        public int Score { get; set; }
        public string Name { get; set; } = "";
    }
}