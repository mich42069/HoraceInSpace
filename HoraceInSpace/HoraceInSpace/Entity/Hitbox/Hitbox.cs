using System;
using HoraceInSpace.Assets;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity.Hitbox;

/// <summary>
/// Hitbox is used to identify collisions between other hitboxes
/// </summary>
/// <param name="radius">Half size of hitbox, can be radius of a circle or side of a square etc.</param>
public abstract class Hitbox(distance radius) : IHitBox
{
    public position Position { get; set; }
    protected virtual distance Radius { get; set; } = radius;
    
    /// <summary>
    /// Checks collision with another hitbox.
    /// </summary>
    /// <param name="hitBox">Hitbox to check collision with.</param>
    /// <returns>True if they are colliding otherwise false</returns>
    public virtual bool CheckHit(IHitBox hitBox)
    {
        if (hitBox is PointHitbox point)
            return CheckHit(point.Position);

        if (hitBox is not CircleHitbox circle)
            return false;

        return WrappedDistanceTo(circle.Position, SpaceValues.WorldSize)
               <= Radius + circle.Radius;
    }

    /// <summary>
    /// Sets the position of the hitbox, should be done before checking for collision.
    /// </summary>
    /// <param name="position">Position to where it is on the grid</param>
    public virtual void SetPosition(position position)
    {
        this.Position = position;
    }

    /// <summary>
    /// Draws the hitbox with a cyan outline.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch responsible for drawing</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        distance worldX = SpaceValues.WorldSize.X;
        distance worldY = SpaceValues.WorldSize.Y;

        distance[] offsetsX =
        [
            -worldX,
            0.Meters(),
            worldX
        ];

        distance[] offsetsY =
        [
            -worldY,
            0.Meters(),
            worldY
        ];

        foreach (var offsetX in offsetsX)
        {
            foreach (var offsetY in offsetsY)
            {
                DrawCircle(
                    spriteBatch,
                    Position + (offsetX, offsetY).At(),
                    Radius);
            }
        }
    }

    private static void DrawCircle(SpriteBatch spriteBatch, position position, distance radius)
    {
        const int segments = 32;

        Vector2 center = new(
            (float)position.X.Value,
            (float)position.Y.Value);

        for (int i = 0; i < segments; i++)
        {
            float a1 = MathHelper.TwoPi * i / segments;
            float a2 = MathHelper.TwoPi * (i + 1) / segments;

            Vector2 p1 = center + (float)radius.Value * new Vector2(
                MathF.Cos(a1),
                MathF.Sin(a1));

            Vector2 p2 = center + (float)radius.Value * new Vector2(
                MathF.Cos(a2),
                MathF.Sin(a2));

            DrawLine(spriteBatch, p1, p2, Color.White);
        }
    }
    
    private static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
        Vector2 edge = end - start;

        spriteBatch.Draw(
            Textures.HitboxColorPixel,
            start,
            null,
            color,
            MathF.Atan2(edge.Y, edge.X),
            Vector2.Zero,
            new Vector2(edge.Length(), 1f),
            SpriteEffects.None,
            0f);
    }
    
    /// <summary>
    /// Same as CheckHit(IHitbox) but instead of considering a whole other hitbox it is considering only one point.
    /// </summary>
    /// <param name="point">Point we want to check if is inside hitbox.</param>
    /// <returns>True if point is inside the Hitbox. false otherwise</returns>
    public bool CheckHit(position point) =>
        WrappedDistanceTo(point, SpaceValues.WorldSize) <= Radius;



    protected distance WrappedDistanceTo(position other, position worldSize)
    {
        distance dx = (Position.X - other.X).Abs();
        distance dy = (Position.Y - other.Y).Abs();

        if (dx > worldSize.X / 2)
            dx = worldSize.X - dx;

        if (dy > worldSize.Y / 2)
            dy = worldSize.Y - dy;

        return Math.Sqrt((dx * dx + dy * dy).Value).Meters();
    }
}