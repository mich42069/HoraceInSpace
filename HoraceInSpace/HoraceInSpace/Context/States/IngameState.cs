using System;
using System.Collections.Generic;
using HoraceInSpace.Entity;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace.Context.States;

public class IngameState : AState
{
    private readonly TimeSpan _easyDifficultyTimeTreshold = 20.Seconds();
    private readonly TimeSpan _mediumDifficultyTimeTreshold = 40.Seconds();
    private readonly TimeSpan _hardDifficultyTimeTreshold = 60.Seconds();
    private readonly TimeSpan _invincibilityLength = 2.Seconds();
    private const int MaxBullets = 10;
    
    private readonly Horace _horace;
    private List<AEntity> _entities = new();
    private List<Bullet> _bullets = new();
    private List<Bullet> _ufoBullets = new();
    
    private readonly double _timeScale;
    private readonly Button _shootButton = new();
    private int _totalScore = 0;
    private bool _invincible = false;
    private TimeSpan _startInvincibility;
    private AState _newState;
    private int _wave = 0;
    
    public IngameState(GameArguments arguments) : base(arguments)
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

        TryKillBullets(adjustedGameTime, ref _bullets);
        TryKillBullets(adjustedGameTime, ref _ufoBullets);
        
        (HashSet<int>, HashSet<int>) hitBulletsEntities = HitregBullets();

        int newScore = GetScoreForHits(hitBulletsEntities.Item2);

        _totalScore += newScore;

        List<AEntity> newAsteroids = SplitUpHitAsteroids(hitBulletsEntities.Item2);
        _entities.AddRange(newAsteroids);
        
        _bullets.MassDeleteFromHashset(hitBulletsEntities.Item1);
        _entities.MassDeleteFromHashset(hitBulletsEntities.Item2);

        TrySpawn(adjustedGameTime);
        TrySpawnUfoBullets();
    }

    private void TrySpawnUfoBullets()
    {
        foreach (var entity in _entities)
        {
            if (entity is AUfo { IsReadyToShoot: true } ufo) 
                _ufoBullets.Add(ufo.Shoot(_horace.Position));
        }
    }

    private void TryKillBullets(GameTime gameTime, ref List<Bullet> bullets)
    {
        HashSet<int> bulletsToDelete = new();
        for (int i = 0; i < bullets.Count; i++)
            if (bullets[i].LifeTimeOver(gameTime)) bulletsToDelete.Add(i);
        bullets.MassDeleteFromHashset(bulletsToDelete);
    }

    private Difficulty GetDifficulty(GameTime gameTime)
    {
        TimeSpan diff = gameTime.ElapsedGameTime;
        if (diff > _hardDifficultyTimeTreshold) return Difficulty.Extreme;
        if (diff > _mediumDifficultyTimeTreshold) return Difficulty.Hard;
        if (diff > _easyDifficultyTimeTreshold) return Difficulty.Medium;
        return Difficulty.Easy;
    }
    
    private void TrySpawn(GameTime gameTime)
    {
        if (_entities.Count != 0)
            return;
        _wave++;
        Difficulty difficulty = GetDifficulty(gameTime);

        int enemyCount = 3 + _wave;

        for (int i = 0; i < enemyCount; i++)
        {
            _entities.Add(EntityFactory.CreateEntity(difficulty, _horace.Position));
        }
    }

    private List<AEntity> SplitUpHitAsteroids(HashSet<int> hits)
    {
        List<AEntity> newAsteroids = new();
        foreach (int i in hits)
        {
            List<AEntity> splitUpAsteroid = _entities[i].SplitUp();
            newAsteroids.AddRange(splitUpAsteroid);
        }
        
        return newAsteroids;
    }

    private int GetScoreForHits(HashSet<int> entities)
    {
        int totalScore = 0;
        foreach (int i in entities)
        {
            AEntity hitEntity = _entities[i];
            if (hitEntity is IScore scorableEntity)
            {
                totalScore += scorableEntity.Score;
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
        if (_horace.Invincible)
        {
            if (gameTime.TotalGameTime - _startInvincibility > _invincibilityLength)
                _invincible = false;
            return;
        }
        
        foreach (AEntity entity in _entities)
        {
            if (_horace.CheckHit(entity))
            {
                HitSequence();
                return;
            }
        }
        
        foreach (Bullet bullet in _ufoBullets)
        {
            if (_horace.CheckHit(bullet))
            {
                HitSequence();
                _ufoBullets.Remove(bullet);
                return;
            }
        }

        void HitSequence()
        {
            if (_horace.GetHit())
            {
                Death();
            }
            _invincible = true;
            _horace.Respawn(_invincible, _invincibilityLength, gameTime.TotalGameTime);
        }
    }

    private void Death()
    {
        SwitchState = true;
        _newState = new DeathState(_totalScore, Arguments);
    }
    
    private void UpdateEntities(GameTime gameTime)
    {
        _horace.Update(gameTime);
        foreach (AEntity entity in _entities) entity.Update(gameTime);
        foreach (Bullet bullet in _bullets) bullet.Update(gameTime);
        foreach (Bullet bullet in _ufoBullets) bullet.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _horace.Draw(spriteBatch);
        foreach (AEntity entity in _entities) entity.Draw(spriteBatch);
        foreach (Bullet bullet in _bullets) bullet.Draw(spriteBatch);
        foreach (Bullet bullet in _ufoBullets) bullet.Draw(spriteBatch);
    }

    public override void CheckInputs(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Death();
        if (Keyboard.GetState().IsKeyDown(Keys.W))
            _horace.Forward();
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            _horace.Back();
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            _horace.Right();
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            _horace.Left();
        if (_shootButton.Update(Mouse.GetState().LeftButton == ButtonState.Pressed, gameTime) &&
            _bullets.Count < MaxBullets)
            _bullets.Add(_horace.Shoot());
    }

    public override AState NewState()
    {
        return _newState;
    }
}