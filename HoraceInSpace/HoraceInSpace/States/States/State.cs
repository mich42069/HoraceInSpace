using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States;

public abstract class State
{
    protected readonly GameArguments Arguments;

    protected readonly SpriteFont Font;
    protected readonly Vector2 ScreenSize;
    protected readonly Vector2 Center;

    public bool SwitchState;
    public bool Exit;

    protected State(GameArguments arguments)
    {
        Arguments = arguments;

        Font = Assets.Assets.Font;
        ScreenSize = SpaceValues.WorldSize.ToVector2();
        Center = ScreenSize / 2f;
    }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void CheckInputs(GameTime gameTime) { }

    public virtual void TextInput(char c) { }

    public abstract State NewState();

    protected void DrawCentered(SpriteBatch spriteBatch, string text, float y, Color color)
    {
        Vector2 size = Font.MeasureString(text);

        spriteBatch.DrawString(
            Font,
            text,
            new Vector2(Center.X - size.X / 2f, y),
            color);
    }

    /// <summary>
    /// Draws text aligned to the right.
    /// </summary>
    protected void DrawRightAligned(SpriteBatch spriteBatch, string text, float x, float y, Color color)
    {
        Vector2 size = Font.MeasureString(text);

        spriteBatch.DrawString(Font, text, new Vector2(x - size.X, y), color);
    }
}

public enum StateEnum
{
    Ingame,
    Menu,
    Scoreboard,
    Death
}