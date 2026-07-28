using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States;

public abstract class State
{
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(SpriteBatch spriteBatch) {}
    public virtual void CheckInputs(GameTime gameTime) {}
    public bool SwitchState = false;
    public bool Exit = false;
    protected GameArguments Arguments;
    public abstract State NewState();

    protected State(GameArguments arguments)
    {
        Arguments = arguments;
    }
    
    public virtual void TextInput(char c)
    {
    }
}

public enum StateEnum
{
    Ingame,
    Menu,
    Scoreboard,
    Death
}