using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States;

/// <summary>
/// Represents state that is then switched by StateSwitcher.
/// </summary>
public abstract class State
{
    protected readonly GameArguments Arguments;

    protected static SpriteFont Font => Assets.Assets.Font;
    protected Vector2 ScreenSize => SpaceValues.ScreenSize;
    protected Vector2 Center => ScreenSize / 2f;

    /// <summary>
    /// True if the next state has been decided and is ready to switch
    /// </summary>
    public bool SwitchState = false;
    
    /// <summary>
    /// True if exit has been called, to be passed upwards.
    /// </summary>
    public bool Exit;

    /// <summary>
    /// Saves the arguments as well as initializes the ScreenSize from a static class.
    /// </summary>
    /// <param name="arguments">Game arguments parsed at startup.</param>
    protected State(GameArguments arguments)
    {
        Arguments = arguments;
    }
    /// <summary>
    /// Base update method, empty by default.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update the state by.</param>
    public virtual void Update(GameTime gameTime) { }
    
    /// <summary>
    /// Draws out everything in the state, empty by default.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public virtual void Draw(SpriteBatch spriteBatch) { }
    
    /// <summary>
    /// Checks keyboard, mouse and controller input and passes it to its members.
    /// </summary>
    /// <param name="gameTime">Current GameTime to account for key spam and else.</param>
    public virtual void CheckInputs(GameTime gameTime) { }

    /// <summary>
    /// Gets the text input from keyboard for writing.
    /// </summary>
    /// <param name="c">Character typed by the keyboard.</param>
    public virtual void TextInput(char c) { }

    /// <summary>
    /// Gives the next state, should be called only when needed.
    /// </summary>
    /// <returns>Returns next state that should be switched to.</returns>
    public abstract State NewState();

    /// <summary>
    /// Draws text alligned to the center
    /// </summary>
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