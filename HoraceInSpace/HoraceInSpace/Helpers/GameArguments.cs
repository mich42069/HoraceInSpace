namespace HoraceInSpace;

/// <summary>
/// Class that holds all arguments the game was started with.
/// </summary>
public class GameArguments
{
    public double TimeScale { get; init; } = 1.0;
    public bool ShowHitboxes { get; init; }

    /// <summary>
    /// Parses the arguments and then holds them.
    /// </summary>
    /// <param name="args">All arguments to be parsed.</param>
    /// <returns>An instance of GameArguments, holding the parsed arguments.</returns>
    public static GameArguments Parse(string[] args)
    {
        double timeScale = 1.0;
        bool showHitboxes = false;
        
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            if (arg is "--hitboxes" or "-h")
            {
                showHitboxes = true;
            }
            else if (arg.StartsWith("--speed="))
            {
                string value = arg["--speed=".Length..];

                if (double.TryParse(value, out double speed) && speed > 0)
                    timeScale = speed;
            }
            else if (arg == "-s" && i + 1 < args.Length)
            {
                if (double.TryParse(args[++i], out double speed) && speed > 0)
                    timeScale = speed;
            }
        }

        return new GameArguments
        {
            TimeScale = timeScale,
            ShowHitboxes = showHitboxes
        };
    }
}