using System;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Entity;

public static class EntityFactory
{
    private static readonly Random Random = new();

    private static readonly distance SpawnDistance = SpaceValues.EnormousAsteroidRadius;
    
    private struct SpawnWeights
    {
        public float SmallAsteroid;
        public float MediumAsteroid;
        public float BigAsteroid;
        public float GiantAsteroid;
        public float EnormousAsteroid;
        public float Ufo;
        public float Sum => SmallAsteroid + MediumAsteroid + BigAsteroid + GiantAsteroid + EnormousAsteroid + Ufo;
        public static SpawnWeights Default => new SpawnWeights
        {
            SmallAsteroid = 1 / 6f,
            MediumAsteroid = 1 / 6f,
            BigAsteroid = 1 / 6f,
            EnormousAsteroid = 1 / 6f,
            GiantAsteroid = 1 / 6f,
            Ufo = 1 / 6f
        };
    }
    private static SpawnWeights GetWeights(Difficulty difficulty)
    {
        SpawnWeights weights = difficulty switch
        {
            Difficulty.Easy => new SpawnWeights
            {
                SmallAsteroid = 0.65f,
                MediumAsteroid = 0.20f,
                BigAsteroid = 0.10f,
                GiantAsteroid = 0.025f,
                EnormousAsteroid = 0.025f,
                Ufo = 0f
            },

            Difficulty.Medium => new SpawnWeights
            {
                SmallAsteroid = 0.45f,
                MediumAsteroid = 0.30f,
                BigAsteroid = 0.15f,
                GiantAsteroid = 0.025f,
                EnormousAsteroid = 0.025f,
                Ufo = 0.05f
            },

            Difficulty.Hard => new SpawnWeights
            {
                SmallAsteroid = 0.30f,
                MediumAsteroid = 0.30f,
                BigAsteroid = 0.20f,
                GiantAsteroid = 0.075f,
                EnormousAsteroid = 0.025f,
                Ufo = 0.10f
            },

            Difficulty.Extreme => new SpawnWeights
            {
                SmallAsteroid = 0.10f,
                MediumAsteroid = 0.30f,
                BigAsteroid = 0.25f,
                GiantAsteroid = 0.10f,
                EnormousAsteroid = 0.05f,
                Ufo = 0.20f
            },

            _ => throw new ArgumentOutOfRangeException()
        };

        return Math.Abs(weights.Sum - 1f) < 0.0001f
            ? weights
            : SpawnWeights.Default;
    }
    private static position GenerateSpawnPosition(position avoidPosition)
    {
        position worldSize = SpaceValues.WorldSize;

        while (true)
        {
            position spawn = (
                Random.NextSingle() * worldSize.X,
                Random.NextSingle() * worldSize.Y
            ).At();

            distance dx = (spawn.X - avoidPosition.X).Abs();
            distance dy = (spawn.Y - avoidPosition.Y).Abs();

            // Take the shortest wrapped distance
            if (dx > worldSize.X / 2)
                dx = worldSize.X - dx;

            if (dy > worldSize.Y / 2)
                dy = worldSize.Y - dy;

            if ((dx * dx + dy * dy).SquareRoot() >= SpawnDistance)
                return spawn;
        }
    }

    private static T CreateEntity<T>(position position,
        Func<position, angle, angle, speed, T> factory)
        where T : Entity
    {
        angle angleOfMotion = (Random.NextSingle() * 360).Degrees();
        angle angleOfRotation = (Random.NextSingle() * 360).Degrees();
        speed initialSpeed = Random.NextSingle() * 200.MetersPerSecond();

        return factory(
            position,
            angleOfMotion,
            angleOfRotation,
            initialSpeed);
    }

    private static NormalUfo CreateUfo(position position) => 
        CreateEntity(position, (p, m, r, s) => 
            new NormalUfo(p, m, r, s));
    private static AsteroidEnormous CreateEnormousAsteroid(position position) => 
        CreateEntity(position, (p, m, r, s) => 
            new AsteroidEnormous(p, m, r, s));

    private static AsteroidGiant CreateGiantAsteroid(position position) => 
        CreateEntity(position, (p, m, r, s) => 
            new AsteroidGiant(p, m, r, s));

    private static AsteroidBig CreateBigAsteroid(position position) => 
        CreateEntity(position, (p, m, r, s) => 
            new AsteroidBig(p, m, r, s));

    private static AsteroidMedium CreateMediumAsteroid(position position) => 
        CreateEntity(position, (p, m, r, s) => 
            new AsteroidMedium(p, m, r, s));

    private static AsteroidSmall CreateSmallAsteroid(position position) => 
        CreateEntity(position, (p, m, r, s) => 
            new AsteroidSmall(p, m, r, s));

    public static Entity CreateEntity(Difficulty difficulty, position avoidPosition)
    {
        position spawnPosition = GenerateSpawnPosition(avoidPosition);

        
        var choices = new (float Weight, Func<Entity> Create)[]
        {
            (GetWeights(difficulty).SmallAsteroid, () => CreateSmallAsteroid(spawnPosition)),
            (GetWeights(difficulty).MediumAsteroid, () => CreateMediumAsteroid(spawnPosition)),
            (GetWeights(difficulty).BigAsteroid, () => CreateBigAsteroid(spawnPosition)),
            (GetWeights(difficulty).GiantAsteroid, () => CreateGiantAsteroid(spawnPosition)),
            (GetWeights(difficulty).EnormousAsteroid, () => CreateEnormousAsteroid(spawnPosition)),
            (GetWeights(difficulty).Ufo, () => CreateUfo(spawnPosition))
        };

        float roll = Random.NextSingle();

        foreach (var choice in choices)
        {
            if (roll < choice.Weight)
                return choice.Create();

            roll -= choice.Weight;
        }

        throw new InvalidOperationException($"Spawn weights of {difficulty} difficulty don't sum up to 1, or Default doesn't sum up to 1.");
    }
}