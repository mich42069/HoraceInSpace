using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Context.States;

public abstract class AState
{
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract void CheckInputs(GameTime gameTime);
    public bool SwitchState = false;
    public bool Exit = false;
    public abstract StateEnum NewState();
}

public enum StateEnum
{
    Ingame,
    Menu,
    Scoreboard,
    Death
}