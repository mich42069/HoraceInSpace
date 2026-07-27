using System;
using HoraceInSpace.Background;
using HoraceInSpace.Context.States;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Context;

public class ContextMachine : IContextMachine
{
    private AState _currentState;
    private readonly StarsBackground _starsBackground;
    private SpriteBatch _spriteBatch;
    private readonly GameArguments _arguments;
    private ParallelWrapper _starsUpdater;

    public ContextMachine(GraphicsDevice device, Vector2 screenSize, int count, GameArguments arguments)
    {
        _arguments = arguments;
        position screenSizeInMeters = ((double)screenSize.X, (double)screenSize.Y).At();
        SpaceValues.WorldSize = screenSizeInMeters;
        _currentState = new MenuState(_arguments);
        _starsBackground = new StarsBackground(device, screenSize, count);
        _starsUpdater = new ParallelWrapper(
            gameTime => _starsBackground.Update(gameTime)
        );
    }
    
    public void TextInput(char character)
    {
        _currentState.TextInput(character);
    }
    
    public void Update(GameTime gameTime)
    {
        _starsUpdater.TriggerUpdate(gameTime);
        
        _currentState.CheckInputs(gameTime);
        _currentState.Update(gameTime);
        if (_currentState.SwitchState)
        {
            KeyboardInput.Clear();
            _currentState = _currentState.NewState();
        }
    }

    public void Draw(GameTime gameTime)
    {
        _starsBackground.Draw(_spriteBatch);
        _currentState.Draw(_spriteBatch);
    }


    public bool CanExit() => _currentState.Exit;
    public void SetSpriteBatch(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }
}