using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class ContextMachine : IContextMachine
{
    private AState _currentState;
    private StarsBackground _starsBackground;
    private SpriteBatch _spriteBatch;

    public ContextMachine(GraphicsDevice device, Vector2 screenSize, int count)
    {
        _currentState = new Ingame();
        _starsBackground = new StarsBackground(device, screenSize, count);
    }
    
    public void Update(GameTime gameTime)
    {
        _starsBackground.Update(gameTime);
        _currentState.Update(gameTime);
        _currentState.CheckInputs();
        if (_currentState.SwitchState) 
            SwitchState(_currentState.NewState());
    }

    public void Draw(GameTime gameTime)
    {
        _starsBackground.Draw(_spriteBatch);
        _currentState.Draw(gameTime);
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