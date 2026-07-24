using System;
using System.Collections.Generic;
using System.Linq;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace.Entity;

public class Horace : AEntity
{
    private bool _isInvincible;
    private TimeSpan _invincibleUntil;
    public bool Invincible => _isInvincible;
    private int _lifes = 3;
    private readonly force AccelerationForce = 20_000_000_000.0.Newtons();
    private readonly List<(acceleration, angle)> _accelerationControl = new ();
    
    public Horace()
    {
        Radius = 20.Meters();
        Hitbox = new CircleHitbox(Radius);
        Density = 2000.KilogramsPerCubicMeter();
        ResetMovementAndPosition();
    }

    public override void Update(GameTime gameTime)
    {
        if (_isInvincible && gameTime.TotalGameTime >= _invincibleUntil)
        {
            _isInvincible = false;
        }
        base.Update(gameTime);
    }

    private void ResetMovementAndPosition()
    {
        Position = (1000, 1000).At();
        Speed = 0.MetersPerSecond();
        Acceleration = 0.MetersPerSecondSquared();
        AngleOfMotion = 0.Degrees();
        AngleOfRotation = AngleToMouse();
    }

    private angle AngleToMouse()
    {
        var mouse = Mouse.GetState();

        double dx = mouse.X - Position.X.Value;
        double dy = mouse.Y - Position.Y.Value;

        return Math.Atan2(dy, dx).Radians();
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
        speed sx = Speed * AngleOfMotion.Cos();
        speed sy = Speed * AngleOfMotion.Sin();

        // Change in speed
        sx += Acceleration * AngleOfAcceleration.Cos() * dt;
        sy += Acceleration * AngleOfAcceleration.Sin() * dt;

        // Convert back to polar form
        Speed = Math.Sqrt(sx.Value * sx.Value + sy.Value * sy.Value).MetersPerSecond();
        AngleOfMotion = Math.Atan2(sy.Value, sx.Value).Radians();
    }
    

    protected override void UpdateRotation()
    {
        AngleOfRotation = AngleToMouse();
    }

    public bool GetHit()
    {
        return (--_lifes < 1);
    }

    public void Respawn(bool isInvincible, TimeSpan invincibilityLength, TimeSpan currentGameTime)
    {
        _isInvincible = isInvincible;
        _invincibleUntil = currentGameTime + invincibilityLength;
        ResetMovementAndPosition();
    }
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        Color color = _isInvincible 
            ? Color.White * 0.5f 
            : Color.White;
        
        spriteBatch.Draw(
            Textures.Horace,
            pos.ToVector2(),
            null,
            color,
            (float)AngleOfRotation.Value,
            Textures.HoraceOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }

    public Bullet Shoot()
    {
        return new Bullet(Position, AngleOfRotation, AngleOfRotation, 1500.MetersPerSecond());
    }

    public override bool CheckHit(AEntity entity)
    {
        return Hitbox.CheckHit(entity.Hitbox);
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