using HoraceInSpace.Background;
using HoraceInSpace.Helpers;
using HoraceInSpace.States.States;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States;

public class StateSwitcher : IStateSwitcher
{
    private State _currentState;
    private readonly StarsBackground _starsBackground;
    private SpriteBatch _spriteBatch;
    private readonly GameArguments _arguments;
    private ParallelWrapper _starsUpdater;

    public StateSwitcher(Vector2 screenSize, int starsCount, GameArguments arguments)
    {
        _arguments = arguments;
        SpaceValues.ScreenSize = screenSize;
        _starsBackground = new StarsBackground(screenSize, starsCount);
        _currentState = new LoadState(arguments, _starsBackground);
        _starsUpdater = new ParallelWrapper(
            gameTime => _starsBackground.Update(gameTime), true);
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

    public void Draw()
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