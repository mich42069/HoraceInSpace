using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;


using Microsoft.Xna.Framework;

public class CircleHitBox(position position, distance radius) : IHitBox
{
    private position Position { get; set; } = position;
    private distance Radius { get; set; } = radius;
    
    
    public bool CheckHit(IHitBox hitBox)
    {
        if (hitBox is CircleHitBox circle)
        {
            distance distanceToCircle = Position.DistanceTo(circle.Position);

            distance radiusSum = Radius + circle.Radius;

            return distanceToCircle <= radiusSum;
        }

        return false;
    }

    public void SetPosition(position position)
    {
        this.Position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        const int segments = 32;

        float radius = (float)Radius.Value;
        Vector2 center = new((float)Position.X.Value, (float)Position.Y.Value);

        for (int i = 0; i < segments; i++)
        {
            float a1 = MathHelper.TwoPi * i / segments;
            float a2 = MathHelper.TwoPi * (i + 1) / segments;

            Vector2 p1 = center + radius * new Vector2(MathF.Cos(a1), MathF.Sin(a1));
            Vector2 p2 = center + radius * new Vector2(MathF.Cos(a2), MathF.Sin(a2));

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
        return !(Position.DistanceTo(point) > Radius);
    }
}