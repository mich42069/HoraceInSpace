using System;
using System.Collections.Generic;
using System.Linq;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace.Entity;

public readonly struct Thruster(position offset, angle angle)
{
    public position Offset { get; } = offset;
    public angle Angle { get; } = angle;
}

public class Horace : Entity
{
    protected override Texture2D Texture => Textures.Horace;
    protected override Vector2 TextureOrigin => Textures.HoraceOrigin;
    protected Texture2D ThrusterTexture => Textures.Thruster;
    protected Vector2 ThrusterTextureOrigin => Textures.ThrusterOrigin;
    private bool _isInvincible;
    private TimeSpan _invincibleUntil;
    public bool Invincible => _isInvincible;
    private int _lives = 3;
    public int Lives => _lives;
    private readonly force _accelerationForce = 160.GigaNewtons();
    private readonly List<(acceleration, angle)> _accelerationControl = new ();
    private readonly List<Thruster> _thrustersToDraw = new();
    
    private ThrusterSoundWrapper _thrusterSoundWrapper = new(Sounds.Thruster.CreateInstance());
    
    private readonly Thruster[] _thrusters =
    {
        // Forward Thruster
        new((-40.Meters(), 0.Meters()).At(), angle.Deg180),

        // Left Thruster
        new((0.Meters(), 20.Meters()).At(), angle.Deg90),

        // Right Thruster
        new((0.Meters(), -44.Meters()).At(), angle.Deg270),
        
        // Back Thruster
        new((50.Meters(), 0.Meters()).At(), angle.Deg0),
    };
    
    public Horace()
    {
        Hitbox = new CircleHitbox(Radius);
        ResetMovementAndPosition();
        Hitbox.SetPosition(Position);
    }

    protected override distance Radius => SpaceValues.HoraceRadius;
    protected override density Density => SpaceValues.HoraceDensity;

    public override void Update(GameTime gameTime)
    {
        if (_isInvincible && gameTime.TotalGameTime >= _invincibleUntil)
            _isInvincible = false;
        UpdateColor();
        UpdateThrusters();
        UpdateThrusterSounds(gameTime);
        base.Update(gameTime);
    }

    private void UpdateThrusterSounds(GameTime gameTime)
    {
        _thrusterSoundWrapper.UpdateThrusterSound(gameTime, _accelerationControl.Count != 0);
    }

    private void UpdateThrusters()
    {
        _thrustersToDraw.Clear();

        foreach (var accelerationAngle in _accelerationControl)
        {
            angle localAcceleration = accelerationAngle.Item2 - AngleOfRotation;

            // Thruster points opposite the acceleration
            angle thrusterAngle = localAcceleration + 180.Degrees();

            foreach (Thruster thruster in _thrusters)
            {
                if (thruster.Angle.Difference(thrusterAngle).Abs() < 1f.Degrees())
                {
                    _thrustersToDraw.Add(thruster);
                    break;
                }
            }
        }
    }

    private void UpdateColor()
    {
        Color = _isInvincible 
            ? Color.White * 0.5f 
            : Color.White;
    }

    private void ResetMovementAndPosition()
    {
        Position = SpaceValues.WorldSize / 2f;
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
        Sounds.HoraceGetsHit.Play();
        return (--_lives < 1);
    }

    public void Respawn(bool isInvincible, TimeSpan invincibilityLength, TimeSpan currentGameTime)
    {
        _isInvincible = isInvincible;
        _invincibleUntil = currentGameTime + invincibilityLength;
        ResetMovementAndPosition();
    }
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        foreach (Thruster thruster in _thrustersToDraw)
        {
            Vector2 offset = Vector2.Transform(
                PhysicsLibToMonogame.ToVector2(thruster.Offset),
                Matrix.CreateRotationZ((float)AngleOfRotation.Value));

            spriteBatch.Draw(
                ThrusterTexture,
                PhysicsLibToMonogame.ToVector2(pos) + offset,
                null,
                Color,
                (float)(AngleOfRotation + thruster.Angle).Value,
                ThrusterTextureOrigin,
                TextureScale,
                SpriteEffects.None,
                0f);
        }
        base.Draw(pos, spriteBatch);
    }

    public Bullet Shoot()
    {
        Sounds.Shoot.Play();
        return new Bullet(Position, AngleOfRotation, AngleOfRotation, 3000.MetersPerSecond())
        {
            Color = Color.Yellow
        };
    }

    public override bool CheckHit(Entity entity)
    {
        return Hitbox.CheckHit(entity.Hitbox);
    }

    public void Forward()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }

    public void Left()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation - angle.Deg90;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }

    public void Right()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation + angle.Deg90;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }

    public void Back()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation + angle.Deg180;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }
}