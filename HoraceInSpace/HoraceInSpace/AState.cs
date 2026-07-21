using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public abstract class AState
{
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract void CheckInputs();
    public bool SwitchState = false;
    public bool Exit = false;
    public abstract StateEnum NewState();
}

public enum StateEnum
{
    Ingame,
    Menu,
    Scoreboard
}