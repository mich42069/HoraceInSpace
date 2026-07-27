using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace HoraceInSpacePhysicsLib;


public readonly struct distance(double value) : 
   IEquatable<distance>, 
   IComparable<distance>, 
   IFormattable,
   IParsable<distance>,
   IAdditionOperators<distance, distance, distance>,
   ISubtractionOperators<distance, distance, distance>,
   IMultiplyOperators<distance, double, distance>,
   IDivisionOperators<distance, distance, double>,
   IDivisionOperators<distance, double, distance>
{
   public double Value { get; } = value;

    public bool Equals(distance other) =>
       Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
       obj is distance other && Equals(other);

    public override int GetHashCode() =>
       Value.GetHashCode();

    public override string ToString() => $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public bool Negative => Value < 0;

    public static distance operator /(distance dist, double scalar) =>
       new(dist.Value / scalar);

    public static double operator /(distance left, distance rightDistance) =>
       left.Value / rightDistance.Value;

    public static distance operator *(distance dist, double scalar) =>
       new(dist.Value * scalar);

    public static distance operator *(double scalar, distance dist) =>
       new(dist.Value * scalar);

    public static distance operator -(distance left, distance right) =>
       new(left.Value - right.Value);

    public static distance operator +(distance left, distance right) =>
       new(left.Value + right.Value);

    public static bool operator >(distance left, distance right) =>
       left.Value > right.Value;
    
    public static bool operator >=(distance left, distance right) =>
       left.Value > right.Value;

    public static bool operator <(distance left, distance right) =>
       left.Value < right.Value;
    
    public static bool operator <=(distance left, distance right) =>
       left.Value <= right.Value;

    public static bool operator ==(distance left, distance right) =>
       left.Value == right.Value;

    public static bool operator !=(distance left, distance right) =>
       left.Value != right.Value;
    
    public static distance operator -(distance dist) =>
       new (-dist.Value);

    public distance Abs() => Negative ? -this : this;

    public static distance operator %(distance left, distance right)
    {
       double value = left.Value % right.Value;

       if (value < 0)
          value += right.Value;

       return new(value);
    }

    // distance / time = speed
    public static speed operator /(distance dist, TimeSpan t) =>
       new speed(dist.Value / t.TotalSeconds);
    
    // distance * distance = area
    public static area operator *(distance left, distance right) =>
       new area(left.Value * right.Value);

    public int CompareTo(distance other) =>
       Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
       $"{Value.ToString(format, provider)} m";

    public static distance Parse(string s, IFormatProvider? provider) =>
       new(double.Parse(s, provider));

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out distance result)
    {
       if (double.TryParse(s, provider, out double value))
       {
          result = new distance(value);
          return true;
       }

       result = default;
       return false;
    }
}
