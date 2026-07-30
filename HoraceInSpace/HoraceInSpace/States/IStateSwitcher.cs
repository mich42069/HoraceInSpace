using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States;

/// <summary>
/// Interface that represents a StateSwitcher, used by the main loop, to handle and draw different states.
/// </summary>
public interface IStateSwitcher
{
    /// <summary>
    /// Updates a state of a current state and as a part of that checks if it should change states.
    /// </summary>
    /// <param name="gameTime">GameTime parameter holds the elapsed and total time of the game, used to update members that are dependent on time.</param>
    public void Update(GameTime gameTime);
    
    /// <summary>
    /// Draws out the current state
    /// </summary>
    public void Draw();
    
    /// <summary>
    /// Returns true or false dependent on in the exit has been called from one of the states. Is passed as this method instead of throwing an exception for safety.
    /// </summary>
    /// <returns>bool if the engine can exit the StateSwitcher</returns>
    public bool CanExit();
    
    /// <summary>
    /// Sets given SpriteBatch as one later used to draw out states.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawcalls.</param>
    public void SetSpriteBatch(SpriteBatch spriteBatch);
    
    /// <summary>
    /// TextInput is used for writing into the app. Passed downwards, calls synonymous function in a current state.
    /// </summary>
    /// <param name="character"></param>
    public void TextInput(char character);
}