namespace HoraceInSpacePhysicsLib;


public struct position(distance x, distance y)
{
    public distance X = x;
    public distance Y = y;
    public override string ToString() => $"({X}, {Y})";
 
    public static position operator +(position left, position right) =>
        new position(left.X + right.X, left.Y + right.Y);
 
    public static position operator -(position left, position right) =>
        new position(left.X - right.X, left.Y - right.Y);
 
    public static position operator *(position pos, double scalar) =>
        new position(pos.X * scalar, pos.Y * scalar);
 
    public static position operator *(double scalar, position pos) =>
        new position(pos.X * scalar, pos.Y * scalar);
 
    public static position operator /(position pos, double scalar) =>
        new position(pos.X / scalar, pos.Y / scalar);
 
    public static bool operator ==(position left, position right) =>
        left.X == right.X && left.Y == right.Y;
 
    public static bool operator !=(position left, position right) =>
        !(left == right);
 
    public static position operator %(position left, position right) =>
        new position(left.X % right.X, left.Y % right.Y);
 
    // straight-line distance between two points
    public distance DistanceTo(position other)
    {
        double dx = this.X.Value - other.X.Value;
        double dy = this.Y.Value - other.Y.Value;
        return new distance(Math.Sqrt(dx * dx + dy * dy));
    }
}
