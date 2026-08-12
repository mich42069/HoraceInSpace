using System.Numerics;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpacePhysicsLib;

/// <summary>
/// Provides extension methods for creating physics units from primitive numeric values.
/// </summary>
public static class UnitExtensions
{
    /// <summary>
    /// Creates a distance value in meters.
    /// </summary>
    /// <param name="meters">The value in meters.</param>
    /// <returns>A distance representing the specified meters.</returns>
    public static distance Meters(this double meters) => new distance(meters);

    /// <summary>
    /// Creates a distance value in meters.
    /// </summary>
    /// <param name="meters">The value in meters.</param>
    /// <returns>A distance representing the specified meters.</returns>
    public static distance Meters(this float meters) => new distance(meters);

    /// <summary>
    /// Creates a distance value in meters.
    /// </summary>
    /// <param name="meters">The value in meters.</param>
    /// <returns>A distance representing the specified meters.</returns>
    public static distance Meters(this int meters) => new distance(meters);

    /// <summary>
    /// Creates a time span measured in seconds.
    /// </summary>
    /// <param name="seconds">The value in seconds.</param>
    /// <returns>A TimeSpan representing the specified seconds.</returns>
    public static TimeSpan Seconds(this double seconds) => TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Creates a time span measured in seconds.
    /// </summary>
    /// <param name="seconds">The value in seconds.</param>
    /// <returns>A TimeSpan representing the specified seconds.</returns>
    public static TimeSpan Seconds(this float seconds) => TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Creates a time span measured in seconds.
    /// </summary>
    /// <param name="seconds">The value in seconds.</param>
    /// <returns>A TimeSpan representing the specified seconds.</returns>
    public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Creates a time span measured in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The value in milliseconds.</param>
    /// <returns>A TimeSpan representing the specified milliseconds.</returns>
    public static TimeSpan Milliseconds(this double milliseconds) => TimeSpan.FromMilliseconds(milliseconds);

    /// <summary>
    /// Creates a time span measured in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The value in milliseconds.</param>
    /// <returns>A TimeSpan representing the specified milliseconds.</returns>
    public static TimeSpan Milliseconds(this float milliseconds) => TimeSpan.FromMilliseconds(milliseconds);

    /// <summary>
    /// Creates a time span measured in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The value in milliseconds.</param>
    /// <returns>A TimeSpan representing the specified milliseconds.</returns>
    public static TimeSpan Milliseconds(this int milliseconds) => TimeSpan.FromMilliseconds(milliseconds);

    /// <summary>
    /// Creates a speed value in meters per second.
    /// </summary>
    /// <param name="mps">The value in meters per second.</param>
    /// <returns>A speed representing the specified meters per second.</returns>
    public static speed MetersPerSecond(this double mps) => new speed(mps);

    /// <summary>
    /// Creates a speed value in meters per second.
    /// </summary>
    /// <param name="mps">The value in meters per second.</param>
    /// <returns>A speed representing the specified meters per second.</returns>
    public static speed MetersPerSecond(this float mps) => new speed(mps);

    /// <summary>
    /// Creates a speed value in meters per second.
    /// </summary>
    /// <param name="mps">The value in meters per second.</param>
    /// <returns>A speed representing the specified meters per second.</returns>
    public static speed MetersPerSecond(this int mps) => new speed(mps);

    /// <summary>
    /// Creates an acceleration value in meters per second squared.
    /// </summary>
    /// <param name="mps2">The value in meters per second squared.</param>
    /// <returns>An acceleration representing the specified value.</returns>
    public static acceleration MetersPerSecondSquared(this double mps2) => new acceleration(mps2);

    /// <summary>
    /// Creates an acceleration value in meters per second squared.
    /// </summary>
    /// <param name="mps2">The value in meters per second squared.</param>
    /// <returns>An acceleration representing the specified value.</returns>
    public static acceleration MetersPerSecondSquared(this float mps2) => new acceleration(mps2);

    /// <summary>
    /// Creates an acceleration value in meters per second squared.
    /// </summary>
    /// <param name="mps2">The value in meters per second squared.</param>
    /// <returns>An acceleration representing the specified value.</returns>
    public static acceleration MetersPerSecondSquared(this int mps2) => new acceleration(mps2);

    /// <summary>
    /// Creates a momentum value in kilogram meters per second.
    /// </summary>
    /// <param name="kgmps">The value in kilogram meters per second.</param>
    /// <returns>A momentum representing the specified value.</returns>
    public static momentum KilogramMetersPerSecond(this double kgmps) => new momentum(kgmps);

    /// <summary>
    /// Creates a momentum value in kilogram meters per second.
    /// </summary>
    /// <param name="kgmps">The value in kilogram meters per second.</param>
    /// <returns>A momentum representing the specified value.</returns>
    public static momentum KilogramMetersPerSecond(this float kgmps) => new momentum(kgmps);

    /// <summary>
    /// Creates a momentum value in kilogram meters per second.
    /// </summary>
    /// <param name="kgmps">The value in kilogram meters per second.</param>
    /// <returns>A momentum representing the specified value.</returns>
    public static momentum KilogramMetersPerSecond(this int kgmps) => new momentum(kgmps);

    /// <summary>
    /// Creates a mass value in kilograms.
    /// </summary>
    /// <param name="kg">The value in kilograms.</param>
    /// <returns>A mass representing the specified kilograms.</returns>
    public static mass Kilograms(this double kg) => new mass(kg);

    /// <summary>
    /// Creates a mass value in kilograms.
    /// </summary>
    /// <param name="kg">The value in kilograms.</param>
    /// <returns>A mass representing the specified kilograms.</returns>
    public static mass Kilograms(this float kg) => new mass(kg);

    /// <summary>
    /// Creates a mass value in kilograms.
    /// </summary>
    /// <param name="kg">The value in kilograms.</param>
    /// <returns>A mass representing the specified kilograms.</returns>
    public static mass Kilograms(this int kg) => new mass(kg);

    /// <summary>
    /// Creates an area value in square meters.
    /// </summary>
    /// <param name="sqm">The value in square meters.</param>
    /// <returns>An area representing the specified value.</returns>
    public static area SquareMeters(this double sqm) => new area(sqm);

    /// <summary>
    /// Creates an area value in square meters.
    /// </summary>
    /// <param name="sqm">The value in square meters.</param>
    /// <returns>An area representing the specified value.</returns>
    public static area SquareMeters(this float sqm) => new area(sqm);

    /// <summary>
    /// Creates an area value in square meters.
    /// </summary>
    /// <param name="sqm">The value in square meters.</param>
    /// <returns>An area representing the specified value.</returns>
    public static area SquareMeters(this int sqm) => new area(sqm);

    /// <summary>
    /// Creates a volume value in cubic meters.
    /// </summary>
    /// <param name="m3">The value in cubic meters.</param>
    /// <returns>A volume representing the specified value.</returns>
    public static volume CubicMeters(this double m3) => new volume(m3);

    /// <summary>
    /// Creates a volume value in cubic meters.
    /// </summary>
    /// <param name="m3">The value in cubic meters.</param>
    /// <returns>A volume representing the specified value.</returns>
    public static volume CubicMeters(this float m3) => new volume(m3);

    /// <summary>
    /// Creates a volume value in cubic meters.
    /// </summary>
    /// <param name="m3">The value in cubic meters.</param>
    /// <returns>A volume representing the specified value.</returns>
    public static volume CubicMeters(this int m3) => new volume(m3);

    /// <summary>
    /// Creates a density value in kilograms per cubic meter.
    /// </summary>
    /// <param name="kgm3">The value in kilograms per cubic meter.</param>
    /// <returns>A density representing the specified value.</returns>
    public static density KilogramsPerCubicMeter(this double kgm3) => new density(kgm3);

    /// <summary>
    /// Creates a density value in kilograms per cubic meter.
    /// </summary>
    /// <param name="kgm3">The value in kilograms per cubic meter.</param>
    /// <returns>A density representing the specified value.</returns>
    public static density KilogramsPerCubicMeter(this float kgm3) => new density(kgm3);

    /// <summary>
    /// Creates a density value in kilograms per cubic meter.
    /// </summary>
    /// <param name="kgm3">The value in kilograms per cubic meter.</param>
    /// <returns>A density representing the specified value.</returns>
    public static density KilogramsPerCubicMeter(this int kgm3) => new density(kgm3);

    /// <summary>
    /// Creates a drag coefficient value.
    /// </summary>
    /// <param name="cd">The drag coefficient value.</param>
    /// <returns>A drag coefficient representing the specified value.</returns>
    public static dragCoefficient DragCoefficient(this double cd) => new dragCoefficient(cd);

    /// <summary>
    /// Creates a drag coefficient value.
    /// </summary>
    /// <param name="cd">The drag coefficient value.</param>
    /// <returns>A drag coefficient representing the specified value.</returns>
    public static dragCoefficient DragCoefficient(this float cd) => new dragCoefficient(cd);

    /// <summary>
    /// Creates a drag coefficient value.
    /// </summary>
    /// <param name="cd">The drag coefficient value.</param>
    /// <returns>A drag coefficient representing the specified value.</returns>
    public static dragCoefficient DragCoefficient(this int cd) => new dragCoefficient(cd);

    /// <summary>
    /// Creates a force value in newtons.
    /// </summary>
    /// <param name="n">The value in newtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force Newtons(this double n) => new force(n);

    /// <summary>
    /// Creates a force value in newtons.
    /// </summary>
    /// <param name="n">The value in newtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force Newtons(this float n) => new force(n);

    /// <summary>
    /// Creates a force value in newtons.
    /// </summary>
    /// <param name="n">The value in newtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force Newtons(this int n) => new force(n);

    /// <summary>
    /// Creates a force value in kilonewtons.
    /// </summary>
    /// <param name="n">The value in kilonewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force KiloNewtons(this double n) => new force((double)((BigInteger)n*1000));

    /// <summary>
    /// Creates a force value in kilonewtons.
    /// </summary>
    /// <param name="n">The value in kilonewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force KiloNewtons(this float n) => new force((double)((BigInteger)n*1000));

    /// <summary>
    /// Creates a force value in kilonewtons.
    /// </summary>
    /// <param name="n">The value in kilonewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force KiloNewtons(this int n) => new force((double)((BigInteger)n*1000));

    /// <summary>
    /// Creates a force value in meganewtons.
    /// </summary>
    /// <param name="n">The value in meganewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force MegaNewtons(this double n) => new force((double)((BigInteger)n*1_000_000));

    /// <summary>
    /// Creates a force value in meganewtons.
    /// </summary>
    /// <param name="n">The value in meganewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force MegaNewtons(this float n) => new force((double)((BigInteger)n*1_000_000));

    /// <summary>
    /// Creates a force value in meganewtons.
    /// </summary>
    /// <param name="n">The value in meganewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force MegaNewtons(this int n) => new force((double)((BigInteger)n*1_000_000));

    /// <summary>
    /// Creates a force value in giganewtons.
    /// </summary>
    /// <param name="n">The value in giganewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force GigaNewtons(this double n) => new force((double)((BigInteger)n*1_000_000_000));

    /// <summary>
    /// Creates a force value in giganewtons.
    /// </summary>
    /// <param name="n">The value in giganewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force GigaNewtons(this float n) => new force((double)((BigInteger)n*1_000_000_000));

    /// <summary>
    /// Creates a force value in giganewtons.
    /// </summary>
    /// <param name="n">The value in giganewtons.</param>
    /// <returns>A force representing the specified value.</returns>
    public static force GigaNewtons(this int n) => new force((double)((BigInteger)n * 1_000_000_000));

    /// <summary>
    /// Creates an angle value in radians.
    /// </summary>
    /// <param name="radians">The value in radians.</param>
    /// <returns>An angle representing the specified radians.</returns>
    public static angle Radians(this double radians) => new(radians);

    /// <summary>
    /// Creates an angle value in radians.
    /// </summary>
    /// <param name="radians">The value in radians.</param>
    /// <returns>An angle representing the specified radians.</returns>
    public static angle Radians(this float radians) => new(radians);

    /// <summary>
    /// Creates an angle value in radians.
    /// </summary>
    /// <param name="radians">The value in radians.</param>
    /// <returns>An angle representing the specified radians.</returns>
    public static angle Radians(this int radians) => new(radians);

    /// <summary>
    /// Creates an angle value from degrees.
    /// </summary>
    /// <param name="degrees">The value in degrees.</param>
    /// <returns>An angle representing the specified degrees.</returns>
    public static angle Degrees(this double degrees) => angle.FromDegrees(degrees);

    /// <summary>
    /// Creates an angle value from degrees.
    /// </summary>
    /// <param name="degrees">The value in degrees.</param>
    /// <returns>An angle representing the specified degrees.</returns>
    public static angle Degrees(this float degrees) => angle.FromDegrees(degrees);

    /// <summary>
    /// Creates an angle value from degrees.
    /// </summary>
    /// <param name="degrees">The value in degrees.</param>
    /// <returns>An angle representing the specified degrees.</returns>
    public static angle Degrees(this int degrees) => angle.FromDegrees(degrees);
}