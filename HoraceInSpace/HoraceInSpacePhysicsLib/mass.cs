namespace HoraceInSpacePhysicsLib;


public struct mass(double value)
{
   public double Value = value;
   public override string ToString() => $"{Value} kg";

   public static mass operator /(mass w, double scalar) =>
      new mass(w.Value / scalar);

   public static double operator /(mass left, mass right) =>
      left.Value / right.Value;

   public static mass operator *(mass w, double scalar) =>
      new mass(w.Value * scalar);

   public static mass operator *(double scalar, mass w) =>
      new(w.Value * scalar);

   public static mass operator -(mass left, mass right) =>
      new mass(left.Value - right.Value);

   public static mass operator +(mass left, mass right) =>
      new mass(left.Value + right.Value);

   public static bool operator >(mass left, mass right) =>
      left.Value > right.Value;

   public static bool operator <(mass left, mass right) =>
      left.Value < right.Value;

   public static bool operator ==(mass left, mass right) =>
      left.Value == right.Value;

   public static bool operator !=(mass left, mass right) =>
      left.Value != right.Value;

   // mass / density = volume
   public static volume operator /(mass m, density d) =>
      new(m.Value / d.Value);

   // force / mass = acceleration
   public static acceleration operator /(force f, mass m) =>
      new acceleration(f.Value / m.Value);

   // mass * acceleration = force
   public static force operator *(mass m, acceleration a) =>
      new force(m.Value * a.Value);

   // acceleration * mass = force
   public static force operator *(acceleration a, mass m) =>
      new force(m.Value * a.Value);

}

