namespace HoraceInSpace;

public class GameArguments
{
    public double TimeScale { get; init; } = 1.0;
    public bool ShowHitboxes { get; init; }

    public static GameArguments Parse(string[] args)
    {
        double timeScale = 1.0;
        bool showHitboxes = false;

        foreach (string arg in args)
        {
            if (arg == "--hitboxes" || arg == "-h")
            {
                showHitboxes = true;
            }
            else if (arg.StartsWith("--speed="))
            {
                string value = arg["--speed=".Length..];

                if (double.TryParse(value, out double speed) && speed > 0)
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