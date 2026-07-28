using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpacePhysicsLib;

public static class UnitExtensions
{
    public static distance Meters(this double meters) => new distance(meters);
    public static distance Meters(this float meters) => new distance(meters);
    public static distance Meters(this int meters) => new distance(meters);

    public static TimeSpan Seconds(this double seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Seconds(this float seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);

    public static TimeSpan Milliseconds(this double milliseconds) => TimeSpan.FromMilliseconds(milliseconds);
    public static TimeSpan Milliseconds(this float milliseconds) => TimeSpan.FromMilliseconds(milliseconds);
    public static TimeSpan Milliseconds(this int milliseconds) => TimeSpan.FromMilliseconds(milliseconds);

    public static speed MetersPerSecond(this double mps) => new speed(mps);
    public static speed MetersPerSecond(this float mps) => new speed(mps);
    public static speed MetersPerSecond(this int mps) => new speed(mps);

    public static acceleration MetersPerSecondSquared(this double mps2) => new acceleration(mps2);
    public static acceleration MetersPerSecondSquared(this float mps2) => new acceleration(mps2);
    public static acceleration MetersPerSecondSquared(this int mps2) => new acceleration(mps2);

    public static momentum KilogramMetersPerSecond(this double kgmps) => new momentum(kgmps);
    public static momentum KilogramMetersPerSecond(this float kgmps) => new momentum(kgmps);
    public static momentum KilogramMetersPerSecond(this int kgmps) => new momentum(kgmps);

    public static mass Kilograms(this double kg) => new mass(kg);
    public static mass Kilograms(this float kg) => new mass(kg);
    public static mass Kilograms(this int kg) => new mass(kg);
    
    public static position At(this (double x, double y) coords) => new position(coords.x.Meters(), coords.y.Meters());
    public static position At(this (distance x, distance y) coords) => new position(coords.x, coords.y);
    public static position At(this (int x, int y) coords) => new position(coords.x.Meters(), coords.y.Meters());
    
    public static area SquareMeters(this double sqm) => new area(sqm);
    public static area SquareMeters(this float sqm) => new area(sqm);
    public static area SquareMeters(this int sqm) => new area(sqm);
    
    public static volume CubicMeters(this double m3) => new volume(m3);
    public static volume CubicMeters(this float m3) => new volume(m3);
    public static volume CubicMeters(this int m3) => new volume(m3);
    
    public static density KilogramsPerCubicMeter(this double kgm3) => new density(kgm3);
    public static density KilogramsPerCubicMeter(this float kgm3) => new density(kgm3);
    public static density KilogramsPerCubicMeter(this int kgm3) => new density(kgm3);
    
    public static dragCoefficient DragCoefficient(this double cd) => new dragCoefficient(cd);
    public static dragCoefficient DragCoefficient(this float cd) => new dragCoefficient(cd);
    public static dragCoefficient DragCoefficient(this int cd) => new dragCoefficient(cd);


    public static force Newtons(this double n) => new force(n);
    public static force Newtons(this float n) => new force(n);
    public static force Newtons(this int n) => new force(n);
    
    public static angle Radians(this double radians) => new(radians);
    public static angle Radians(this float radians) => new(radians);
    public static angle Radians(this int radians) => new(radians);
    public static angle Degrees(this double degrees) => angle.FromDegrees(degrees);
    public static angle Degrees(this float degrees) => angle.FromDegrees(degrees);
    public static angle Degrees(this int degrees) => angle.FromDegrees(degrees);
}