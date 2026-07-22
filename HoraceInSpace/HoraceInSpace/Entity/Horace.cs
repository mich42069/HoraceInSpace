using System;
using System.Collections.Generic;
using System.Linq;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Horace : AEntity
{
    private readonly force AccelerationForce = 1_000_000.Newtons();
    private List<(acceleration, angle)> _accelerationControl = new ();
    public Horace()
    {
        Radius = 20.Meters();
        Position = (1000, 1000).At();
        HitBox = new CircleHitBox(Position, Radius);
        Speed = 0.MetersPerSecond();
        Acceleration = 0.MetersPerSecondSquared();
        Mass = 3500.Kilograms();
        Area = Radius * Radius * Math.PI;
        AngleOfMotion = 0.Degrees();
        AngleOfRotation = 90.Degrees();
    }

    protected override void UpdateAcceleration()
    {
        acceleration negativeDragAcceleration = force.AtmosphericDrag(SpaceValues.AtmosphericDensity, SpaceValues.DragCoefficient, Area, Speed) / Mass;
        _accelerationControl.Append((negativeDragAcceleration, AngleOfMotion));
        
        (Acceleration, AngleOfAcceleration) = SumAcceleration();
        _accelerationControl.Clear();
    }

    private (acceleration, angle) SumAcceleration()
    {
        double x = 0;
        double y = 0;

        foreach (var (acceleration, angle) in _accelerationControl)
        {
            x += acceleration.Value * angle.Cos();
            y += acceleration.Value * angle.Sin();
        }

        return (new acceleration(Math.Sqrt(x * x + y * y)), new angle(Math.Atan2(y, x)));
    }

    protected override void UpdateSpeed(GameTime gameTime)
    {
        var dt = gameTime.ElapsedGameTime;

        // Current speed
        double sx = Speed.Value * AngleOfMotion.Cos();
        double sy = Speed.Value * AngleOfMotion.Sin();

        // Change in speed
        sx += Acceleration.Value * AngleOfAcceleration.Cos() * dt.TotalSeconds;
        sy += Acceleration.Value * AngleOfAcceleration.Sin() * dt.TotalSeconds;

        // Convert back to polar form
        Speed = new speed(Math.Sqrt(sx * sx + sy * sy));
        AngleOfMotion = new angle(Math.Atan2(sy, sx));
    }
    

    protected override void UpdateRotation()
    {
        var mouse = Mouse.GetState();

        double dx = mouse.X - Position.X.Value;
        double dy = mouse.Y - Position.Y.Value;

        AngleOfRotation = Math.Atan2(dy, dx).Radians();
    }
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Textures.Horace,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            Textures.HoraceOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }

    public override bool CheckHit(AEntity entity)
    {
        return HitBox.CheckHit(entity.HitBox);
    }

    public void Forward()
    {
        var acceleration = AccelerationForce/Mass;
        _accelerationControl.Add((acceleration, AngleOfRotation));
    }

    public void Left()
    {
        var acceleration = AccelerationForce/Mass;
        _accelerationControl.Add((acceleration, AngleOfRotation - angle.Deg90));
    }

    public void Right()
    {
        var acceleration = AccelerationForce/Mass;
        _accelerationControl.Add((acceleration, AngleOfRotation + angle.Deg90));
    }

    public void Back()
    {
        var acceleration = AccelerationForce/Mass;
        _accelerationControl.Add((acceleration, AngleOfRotation + angle.Deg180));
    }
}