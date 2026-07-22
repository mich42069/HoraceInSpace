using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class ContextMachine : IContextMachine
{
    private AState _currentState;
    private StarsBackground _starsBackground;
    private SpriteBatch _spriteBatch;
    private GameArguments _arguments;

    public ContextMachine(GraphicsDevice device, Vector2 screenSize, int count, GameArguments arguments)
    {
        _arguments = arguments;
        position screenSizeInMeters = ((double)screenSize.X, (double)screenSize.Y).At();
        SpaceValues.WorldSize = screenSizeInMeters;
        _currentState = new Ingame(_arguments);
        _starsBackground = new StarsBackground(device, screenSize, count);
    }
    
    public void Update(GameTime gameTime)
    {
        _starsBackground.Update(gameTime);
        _currentState.CheckInputs();
        _currentState.Update(gameTime);
        if (_currentState.SwitchState) 
            SwitchState(_currentState.NewState());
    }

    public void Draw(GameTime gameTime)
    {
        _starsBackground.Draw(_spriteBatch);
        _currentState.Draw(_spriteBatch);
    }

    
    private void SwitchState(StateEnum newState)
    {
        throw new NotImplementedException();
    }


    public bool CanExit() => _currentState.Exit;
    public void SetSpriteBatch(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }
}