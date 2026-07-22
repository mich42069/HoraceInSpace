using System;
using System.Collections.Generic;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Ingame : AState
{
    private Horace _horace;
    private List<AEntity> _entities = new List<AEntity>();
    private position _screenSize;
    public Ingame(position screenSize)
    {
        _screenSize = screenSize;
        _horace = new Horace(screenSize);
    }
    
    public override void Update(GameTime gameTime)
    {
        _horace.Update(gameTime);
        foreach (AEntity entity in _entities)
        {
            if (_horace.CheckHit(entity))
            {
                Console.WriteLine("HIT"); // TODO PROPER HIT
                break;
            }
        }
        foreach (AEntity entity in _entities) entity.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _horace.Draw(spriteBatch);
        foreach (AEntity entity in _entities) entity.Draw(spriteBatch);
    }

    public override void CheckInputs()
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
            _entities.Add(AsteroidFactory.CreateAsteroid(_screenSize));
    }

    public override StateEnum NewState()
    {
        throw new System.NotImplementedException();
    }
}