using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Ingame : AState
{
    private Horace _horace;
    private List<AEntity> _entities = new();
    private List<Bullet> _bullets = new();
    private readonly double _timeScale;
    private Button _shootButton = new();
    private int TotalScore = 0;
    private bool _invincible = false;
    private TimeSpan _startInvincibility;
    private readonly TimeSpan _invincibilityLength = 2.Seconds();
    private StateEnum _nextState;
    
    public Ingame(GameArguments arguments)
    {
        _timeScale = arguments.TimeScale;
        AEntity.DrawHitbox = arguments.ShowHitboxes;
        _horace = new Horace();
    }
    
    public override void Update(GameTime gameTime)
    {
        GameTime adjustedGameTime = new GameTime(gameTime.TotalGameTime * _timeScale, gameTime.ElapsedGameTime * _timeScale);

        UpdateEntities(adjustedGameTime);
        
        HitregHorace(adjustedGameTime);

        (HashSet<int>, HashSet<int>) hitBulletsEntities = HitregBullets();

        int newScore = GetScoreForHits(hitBulletsEntities.Item2);

        TotalScore += newScore;
        
        _bullets.MassDeleteFromHashset(hitBulletsEntities.Item1);
        _entities.MassDeleteFromHashset(hitBulletsEntities.Item2);
    }


    private int GetScoreForHits(HashSet<int> entities)
    {
        int totalScore = 0;
        foreach (int i in entities)
        {
            AEntity hitEntity = _entities[i];
            switch (hitEntity)
            {
                case AsteroidSmall asteroid:
                    totalScore += asteroid.Score;
                    break;

                case AsteroidMedium asteroid:
                    totalScore += asteroid.Score;
                    break;

                case AsteroidBig asteroid:
                    totalScore += asteroid.Score;
                    break;

                case Ufo ufo:
                    totalScore += ufo.Score;
                    break;
            }
        }

        return totalScore;
    }
    
    private (HashSet<int>, HashSet<int>) HitregBullets()
    {
        HashSet<int> bulletsHit = new();
        HashSet<int> entitiesHit = new();

        for (int i = 0; i < _bullets.Count; i++)
        {
            for (int j = 0; j < _entities.Count; j++)
            {
                if (_entities[j].CheckHit(_bullets[i]))
                {
                    bulletsHit.Add(i);
                    entitiesHit.Add(j);
                    break; // Bullet disappears after first hit
                }
            }
        }
        return (bulletsHit, entitiesHit);
    }

    private void HitregHorace(GameTime gameTime)
    {
        if (_invincible)
        {
            if (gameTime.TotalGameTime - _startInvincibility > _invincibilityLength)
                _invincible = false;
        }
        
        foreach (AEntity entity in _entities)
        {
            if (_horace.CheckHit(entity))
            {
                if (_horace.GetHit())
                {
                    SwitchState = true;
                    _nextState = StateEnum.Death;
                }
                _invincible = true;
                _horace.Respawn(_invincible, _invincibilityLength);
                _startInvincibility = gameTime.TotalGameTime;
                break;
            }
        }
    }
    
    private void UpdateEntities(GameTime gameTime)
    {
        _horace.Update(gameTime);
        foreach (AEntity entity in _entities) entity.Update(gameTime);
        foreach (Bullet bullet in _bullets) bullet.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _horace.Draw(spriteBatch);
        foreach (AEntity entity in _entities) entity.Draw(spriteBatch);
        foreach (Bullet bullet in _bullets) bullet.Draw(spriteBatch);
    }

    public override void CheckInputs(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit = true;
        if (Keyboard.GetState().IsKeyDown(Keys.W))
            _horace.Forward();
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            _horace.Back();
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            _horace.Right();
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            _horace.Left();
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
            _entities.Add(AsteroidFactory.CreateAsteroid());
        if (_shootButton.Update(Mouse.GetState().LeftButton == ButtonState.Pressed, gameTime))
            _bullets.Add(_horace.Shoot());
    }

    public override StateEnum NewState()
    {
        return _nextState;
    }
}