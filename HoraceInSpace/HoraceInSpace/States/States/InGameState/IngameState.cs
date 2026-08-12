using System;
using System.Collections.Generic;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace.States.States.InGameState;

/// <summary>
/// Instance of State that is responsible for the actual gameplay.
/// </summary>
public class IngameState : State
{
    private readonly TimeSpan _easyDifficultyTimeThreshold = 20.Seconds();
    private readonly TimeSpan _mediumDifficultyTimeThreshold = 40.Seconds();
    private readonly TimeSpan _hardDifficultyTimeThreshold = 60.Seconds();
    private readonly TimeSpan _invincibilityLength = 2.Seconds();
    private const int MaxBullets = 10;
    
    private readonly Horace _horace;
    private List<Entity.Entity> _entities = new();
    private List<Bullet> _bullets = new();
    private List<Bullet> _ufoBullets = new();
    
    private readonly double _timeScale;
    private readonly Button _shootButton = new();
    private int _totalScore = 0;
    private bool _invincible = false;
    private TimeSpan _startInvincibility;
    private State _newState;
    private int _wave = 0;
    private List<IDrawableStat> _drawableStat = new();
    
    /// <summary>
    /// Initializes the state, drawable stats and sets timescale and drawing of hitboxes from arguments
    /// </summary>
    /// <param name="arguments">Game arguments parsed at startup.</param>
    public IngameState(GameArguments arguments) : base(arguments)
    {
        _timeScale = arguments.TimeScale;
        Entity.Entity.DrawHitbox = arguments.ShowHitboxes;
        _horace = new Horace();
        _drawableStat.Add(new LiveStat(_horace));
        _drawableStat.Add(new ScoreStat(() => _totalScore));
        _drawableStat.Add(new DifficultyStat(GetDifficulty));
    }
    
    /// <summary>
    /// Updates the game by first updating all entity Positions, then checking for collisions of horace,
    /// then bullets, then tries to split up and give score for all entities killed in this frame.
    /// If there are no enemies on screen tries to spawn new ones based on the difficulty.
    /// Lastly updates all DrawableStats.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update the state by.</param>
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

        List<Entity.Entity> newAsteroids = SplitUpHitEntities(hitBulletsEntities.Item2);
        _entities.AddRange(newAsteroids);
        
        _bullets.MassDeleteFromHashset(hitBulletsEntities.Item1);
        _entities.MassDeleteFromHashset(hitBulletsEntities.Item2);

        TrySpawn(adjustedGameTime);
        TrySpawnUfoBullets();

        foreach (var stat in _drawableStat)
            stat.Update(adjustedGameTime);
    }

    private void TrySpawnUfoBullets()
    {
        foreach (var entity in _entities)
        {
            if (entity is Ufo { IsReadyToShoot: true } ufo) 
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
        TimeSpan diff = gameTime.TotalGameTime;
        if (diff > _hardDifficultyTimeThreshold) return Difficulty.Extreme;
        if (diff > _mediumDifficultyTimeThreshold) return Difficulty.Hard;
        if (diff > _easyDifficultyTimeThreshold) return Difficulty.Medium;
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

    private List<Entity.Entity> SplitUpHitEntities(HashSet<int> hits)
    {
        List<Entity.Entity> newAsteroids = new();
        foreach (int i in hits)
        {
            Sounds.Explosion.Play();
            List<Entity.Entity> splitUpAsteroid = _entities[i].SplitUp();
            newAsteroids.AddRange(splitUpAsteroid);
        }
        
        return newAsteroids;
    }

    private int GetScoreForHits(HashSet<int> entities)
    {
        int totalScore = 0;
        foreach (int i in entities)
        {
            Entity.Entity hitEntity = _entities[i];
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
        
        foreach (Entity.Entity entity in _entities)
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
        foreach (Entity.Entity entity in _entities) entity.Update(gameTime);
        foreach (Bullet bullet in _bullets) bullet.Update(gameTime);
        foreach (Bullet bullet in _ufoBullets) bullet.Update(gameTime);
    }

    /// <summary>
    /// Draws out all entities and DrawableStats.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        _horace.Draw(spriteBatch);
        foreach (Entity.Entity entity in _entities) entity.Draw(spriteBatch);
        foreach (Bullet bullet in _bullets) bullet.Draw(spriteBatch);
        foreach (Bullet bullet in _ufoBullets) bullet.Draw(spriteBatch);
        foreach (IDrawableStat stat in _drawableStat) stat.Draw(spriteBatch);
    }

    /// <summary>
    /// Checks the players inputs from keyboard and mouse, to shoot move and exit the game.
    /// </summary>
    /// <param name="gameTime"></param>
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

    /// <summary>
    /// Returns next state, it being the DeathState, when ready.
    /// </summary>
    /// <returns>Next state</returns>
    public override State NewState()
    {
        return _newState;
    }
}