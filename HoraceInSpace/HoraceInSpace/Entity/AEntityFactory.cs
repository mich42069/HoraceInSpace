using System;
using HoraceInSpacePhysicsLib;

namespace HoraceInSpace.Entity;

public static class EntityFactory
{
    private static readonly Random _random = new();

    private static readonly distance _spawnDistance = 100.Meters();
    
    private struct SpawnWeights
    {
        public float SmallAsteroid;
        public float MediumAsteroid;
        public float BigAsteroid;
        public float Ufo;
    }
    
    private static SpawnWeights GetWeights(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => new SpawnWeights
            {
                SmallAsteroid = 0.70f,
                MediumAsteroid = 0.20f,
                BigAsteroid = 0.1f,
                Ufo = 0f
            },

            Difficulty.Medium => new SpawnWeights
            {
                SmallAsteroid = 0.50f,
                MediumAsteroid = 0.30f,
                BigAsteroid = 0.15f,
                Ufo = 0.05f
            },

            Difficulty.Hard => new SpawnWeights
            {
                SmallAsteroid = 0.35f,
                MediumAsteroid = 0.35f,
                BigAsteroid = 0.20f,
                Ufo = 0.10f
            },

            Difficulty.Extreme => new SpawnWeights
            {
                SmallAsteroid = 0.1f,
                MediumAsteroid = 0.35f,
                BigAsteroid = 0.30f,
                Ufo = 0.25f
            },

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static position GenerateSpawnPosition(position avoidPosition)
    {
        position worldSize = SpaceValues.WorldSize;
        position spawn;
        do
        {
            spawn = (_random.NextSingle() * worldSize.X, _random.NextSingle() * worldSize.Y).At();

        } while (spawn.DistanceTo(avoidPosition) < _spawnDistance);

        return spawn;
    }

    private static T CreateEntity<T>(position position,
        Func<position, angle, angle, speed, T> factory)
        where T : AEntity
    {
        angle angleOfMotion = (_random.NextSingle() * 360).Degrees();
        angle angleOfRotation = (_random.NextSingle() * 360).Degrees();
        speed initialSpeed = _random.NextSingle() * 200.MetersPerSecond();

        return factory(
            position,
            angleOfMotion,
            angleOfRotation,
            initialSpeed);
    }

    private static Ufo CreateUfo(position position) => CreateEntity(position, (p, m, r, s) => new Ufo(p, m, r, s));

    private static AsteroidBig CreateBigAsteroid(position position) => CreateEntity(position, (p, m, r, s) => new AsteroidBig(p, m, r, s));

    private static AsteroidMedium CreateMediumAsteroid(position position) => CreateEntity(position, (p, m, r, s) => new AsteroidMedium(p, m, r, s));

    private static AsteroidSmall CreateSmallAsteroid(position position) => CreateEntity(position, (p, m, r, s) => new AsteroidSmall(p, m, r, s));

    public static AEntity CreateEntity(Difficulty difficulty, position avoidPosition)
    {
        position spawnPosition = GenerateSpawnPosition(avoidPosition);

        var choices = new (float Weight, Func<AEntity> Create)[]
        {
            (GetWeights(difficulty).SmallAsteroid, () => CreateSmallAsteroid(spawnPosition)),
            (GetWeights(difficulty).MediumAsteroid, () => CreateMediumAsteroid(spawnPosition)),
            (GetWeights(difficulty).BigAsteroid, () => CreateBigAsteroid(spawnPosition)),
            (GetWeights(difficulty).Ufo, () => CreateUfo(spawnPosition))
        };

        float roll = _random.NextSingle();

        foreach (var choice in choices)
        {
            if (roll < choice.Weight)
                return choice.Create();

            roll -= choice.Weight;
        }

        throw new InvalidOperationException("Spawn weights must sum to 1.");
    }
}