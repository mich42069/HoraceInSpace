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

public readonly struct Thruster(Position offset, angle angle)
{
    public Position Offset { get; } = offset;
    public angle Angle { get; } = angle;
}

/// <summary>
/// Represents Horace the main character, and playable entity.
/// </summary>
public class Horace : Entity
{
    /// <summary>
    /// How many lives does Horace have left.
    /// </summary>
    public int Lives => _lives;
    /// <summary>
    /// If is currently invincible, and should not be taking damage.
    /// </summary>
    public bool Invincible => _isInvincible;
    protected override Texture2D Texture => Textures.Horace;
    protected override Vector2 TextureOrigin => Textures.HoraceOrigin;
    protected Texture2D ThrusterTexture => Textures.Thruster;
    protected Vector2 ThrusterTextureOrigin => Textures.ThrusterOrigin;
    private bool _isInvincible;
    private TimeSpan _invincibleUntil;
    private int _lives = 3;
    private force _accelerationForce => SpaceValues.HoraceThrusterForce;
    private readonly List<(acceleration, angle)> _accelerationControl = new ();
    private readonly List<Thruster> _thrustersToDraw = new();
    
    private SoundWrapper _thrusterSoundWrapper = new(Sounds.Thruster.CreateInstance());

    /// <summary>
    /// Represents the offset of thrusters to Horace's texture.
    /// </summary>
    private readonly Thruster[] _thrusters =
    [
        // Forward Thruster
        new((-40, 0).At(), angle.Deg180),

        // Left Thruster
        new((0, 20).At(), angle.Deg90),

        // Right Thruster
        new((0, -44).At(), angle.Deg270),

        // Back Thruster
        new((50, 0).At(), angle.Deg0)
    ];
    
    /// <summary>
    /// Public constructor to set basic parameters, differently to other entities, all of Horaces parameters are known in advance.
    /// </summary>
    public Horace()
    {
        Hitbox = new CircleHitbox(Radius);
        ResetMovementAndPosition();
        Hitbox.Position = Position;
    }

    protected override distance Radius => SpaceValues.HoraceRadius;
    protected override density Density => SpaceValues.HoraceDensity;

    /// <summary>
    /// Overrides the original update to add the update of color, thrusters, invincibility, and thruster sounds.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update with.</param>
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
        _thrusterSoundWrapper.UpdateSound(gameTime, _accelerationControl.Count != 0);
    }

    private void UpdateThrusters()
    {
        _thrustersToDraw.Clear();

        foreach (var accelerationAngle in _accelerationControl)
        {
            angle localAccelerationAngle = accelerationAngle.Item2 - AngleOfRotation;

            // Thruster points opposite the acceleration
            angle thrusterAngle = localAccelerationAngle + 180.Degrees();

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

    /// <summary>
    /// Sets base values for Horace.
    /// </summary>
    private void ResetMovementAndPosition()
    {
        Position = SpaceValues.WorldSize / 2f;
        Speed = 0.MetersPerSecond();
        Acceleration = 0.MetersPerSecondSquared();
        AngleOfMotion = 0.Degrees();
        AngleOfRotation = AngleToMouse();
    }

    /// <summary>
    /// Calculates the angle of Horace to cursor Position.
    /// </summary>
    /// <returns>The angle at which we can find the cursor.</returns>
    private angle AngleToMouse()
    {
        var mouse = Mouse.GetState();

        Vector2 onScreenPos = Position.ToVector2();
        
        double dx = mouse.X - onScreenPos.X;
        double dy = mouse.Y - onScreenPos.Y;

        return Math.Atan2(dy, dx).Radians();
    }

    /// <summary>
    /// Overrides the update method, to add the acceleration from thrusters. (players movement)
    /// </summary>
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

    /// <summary>
    /// Overrides the UpdateSpeed base method to account for acceleration.
    /// Also updates the vector of speed.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update with.</param>
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
    
    /// <summary>
    /// Overrides the base method, to set the rotation to look at the mouse.
    /// </summary>
    protected override void UpdateRotation()
    {
        AngleOfRotation = AngleToMouse();
    }

    /// <summary>
    /// Plays hit sound, subtracts one life and checks if the lives Horace has left are 0.
    /// </summary>
    /// <returns>True if Horace has less then 1 live.</returns>
    public bool GetHit()
    {
        Sounds.HoraceGetsHit.Play();
        bool dead = (--_lives < 1);
        if (dead) _thrusterSoundWrapper.Dispose();
        return dead;
    }

    /// <summary>
    /// Respawns Horace with invincibility.
    /// </summary>
    /// <param name="isInvincible">If Horace should become invincible</param>
    /// <param name="invincibilityLength">If he is invincible, how long he should be invincible.</param>
    /// <param name="currentGameTime">Current game time, so that the length of invincibility has a start time.</param>
    public void Respawn(bool isInvincible, TimeSpan invincibilityLength, TimeSpan currentGameTime)
    {
        _isInvincible = isInvincible;
        _invincibleUntil = currentGameTime + invincibilityLength;
        ResetMovementAndPosition();
    }
    
    /// <summary>
    /// Overrides the base draw method to include drawing the thrusters.
    /// </summary>
    /// <param name="pos">Position of where to draw Horace</param>
    /// <param name="spriteBatch">SpriteBatch responsible for drawing Horace.</param>
    protected override void Draw(Position pos, SpriteBatch spriteBatch)
    {
        foreach (Thruster thruster in _thrustersToDraw)
        {
            Vector2 offset = Vector2.Transform(
                new Vector2((float)thruster.Offset.X.Value, (float)thruster.Offset.Y.Value),
                Matrix.CreateRotationZ((float)AngleOfRotation.Value));

            spriteBatch.Draw(
                ThrusterTexture,
                (pos.ToVector2() + offset),
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

    /// <summary>
    /// Returns bullet that has been shot by this method.
    /// </summary>
    /// <returns>The returned bullet with same direction as where Horace is looking.</returns>
    public Bullet Shoot()
    {
        Sounds.Shoot.Play();

        Position bulletPosition = Position + new Position(
            (Radius.Value * AngleOfRotation.Cos()).Meters(),
            (Radius.Value * AngleOfRotation.Sin()).Meters());

        return new Bullet(
            bulletPosition,
            AngleOfRotation,
            AngleOfRotation,
            SpaceValues.BulletInitialSpeed)
        {
            Color = Color.Yellow
        };
    }

    /// <summary>
    /// Accelerates Horace Forward, to where he is looking.
    /// </summary>
    public void Forward()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }

    /// <summary>
    /// Accelerates Horace Left of where he is looking.
    /// </summary>
    public void Left()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation - angle.Deg90;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }

    /// <summary>
    /// Accelerates Horace Right of where he is looking.
    /// </summary>
    public void Right()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation + angle.Deg90;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }

    /// <summary>
    /// Accelerates Horace Back from where he is looking.
    /// </summary>
    public void Back()
    {
        var acceleration = _accelerationForce/Mass;
        var accelerationAngle = AngleOfRotation + angle.Deg180;
        _accelerationControl.Add((acceleration, accelerationAngle));
    }
}