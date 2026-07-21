using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Horace : AEntity
{
    private readonly force AccelerationForce = 10000.Newtons();
    public Horace()
    {
        distance radius = 20.Meters();
        Position = (1000, 1000).At();
        HitBox = new CircleHitBox(Position, radius);
        Speed = 0.MetersPerSecond();
        Acceleration = 0.MetersPerSecondSquared();
        Mass = 100.Kilograms();
        Area = radius * radius;
        AngleOfMotion = 0.Degrees();
        AngleOfRotation = 0.Degrees();
    }
    
    public override void Update(GameTime gameTime)
    {
        UpdateSpeed(gameTime);
        UpdatePosition(gameTime);
        UpdateRotation();
        Console.WriteLine(Acceleration);
        Console.WriteLine(Speed);
        Acceleration = 0.MetersPerSecondSquared();
    }

    private void UpdateSpeed(GameTime gameTime)
    {
        acceleration negativeDragAcceleration = force.AtmosphericDrag(SpaceValues.AtmosphericDensity, SpaceValues.DragCoefficient, Area, Speed) / Mass;
        Acceleration -= negativeDragAcceleration;
        
        time frameTime = gameTime.ElapsedGameTime.Milliseconds.Milliseconds();
        Speed += frameTime * Acceleration;
    }
    
    private void UpdatePosition(GameTime gameTime)
    {
        time dt = gameTime.ElapsedGameTime.TotalSeconds.Seconds();

        Position = new position(
            Position.X + Speed * dt * AngleOfMotion.Cos(),
            Position.Y + Speed * dt * AngleOfMotion.Sin());
    }

    private void UpdateRotation()
    {
        var mouse = Mouse.GetState();

        double dx = mouse.X - Position.X.Value;
        double dy = mouse.Y - Position.Y.Value;

        AngleOfMotion = Math.Atan2(dy, dx).Radians();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Textures.Horace, Position.ToVector2(), Color.White);
    }

    public override bool CheckHit()
    {
        throw new System.NotImplementedException();
    }

    public void Forward()
    {
        Acceleration = AccelerationForce/Mass;
    }

    public void Left()
    {
        
    }

    public void Right()
    {
        
    }

    public void Back()
    {
        Acceleration = -1 * AccelerationForce/Mass;
    }
}