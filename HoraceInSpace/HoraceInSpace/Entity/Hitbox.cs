using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public abstract class Hitbox(distance radius) : IHitBox
{
    protected position Position { get; set; }
    protected distance Radius { get; set; } = radius;


    public virtual bool CheckHit(IHitBox hitBox)
    {
        if (hitBox is not CircleHitbox circle)
            return false;

        distance dx = Position.X - circle.Position.X;
        distance dy = Position.Y - circle.Position.Y;
        
        if (dx.Negative) dx *= -1;
        if (dy.Negative) dx *= -1;

        if (dx > SpaceValues.WorldSize.X / 2)
            dx = SpaceValues.WorldSize.X - dx;

        if (dy > SpaceValues.WorldSize.Y / 2)
            dy = SpaceValues.WorldSize.Y - dy;

        distance distanceToCircle = Math.Sqrt((dx * dx + dy * dy).Value).Meters();

        return distanceToCircle <= Radius + circle.Radius;
    }

    public virtual void SetPosition(position position)
    {
        this.Position = position;
    }

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
            Textures.Pixel,
            start,
            null,
            color,
            MathF.Atan2(edge.Y, edge.X),
            Vector2.Zero,
            new Vector2(edge.Length(), 1f),
            SpriteEffects.None,
            0f);
    }

    public bool CheckHit(position point)
    {
        return !(Position.DistanceTo(point) > Radius); // TODO AROUND EDGES
    }
}