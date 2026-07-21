namespace HoraceInSpacePhysicsLib;

public static class UnitExtensions
{
    public static distance Meters(this double meters) => new distance(meters);
    public static distance Meters(this float meters) => new distance(meters);
    public static distance Meters(this int meters) => new distance(meters);

    public static time Seconds(this double seconds) => new time(seconds);
    public static time Seconds(this float seconds) => new time(seconds);
    public static time Seconds(this int seconds) => new time(seconds);

    public static time Milliseconds(this double milliseconds) => new time(milliseconds / 1000);
    public static time Milliseconds(this float milliseconds) => new time(milliseconds / 1000);
    public static time Milliseconds(this int milliseconds) => new time(milliseconds / 1000);

    public static speed MetersPerSecond(this double mps) => new speed(mps);
    public static speed MetersPerSecond(this float mps) => new speed(mps);
    public static speed MetersPerSecond(this int mps) => new speed(mps);

    public static acceleration MetersPerSecondSquared(this double mps2) => new acceleration(mps2);
    public static acceleration MetersPerSecondSquared(this float mps2) => new acceleration(mps2);
    public static acceleration MetersPerSecondSquared(this int mps2) => new acceleration(mps2);

    public static momentum KilogramMetersPerSecond(this double kgmps) => new momentum(kgmps);
    public static momentum KilogramMetersPerSecond(this float kgmps) => new momentum(kgmps);
    public static momentum KilogramMetersPerSecond(this int kgmps) => new momentum(kgmps);

    public static weight Kilograms(this double kg) => new weight(kg);
    public static weight Kilograms(this float kg) => new weight(kg);
    public static weight Kilograms(this int kg) => new weight(kg);
}