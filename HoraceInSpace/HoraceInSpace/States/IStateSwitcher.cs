using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States;

public interface IStateSwitcher
{
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
    public bool CanExit();
    public void SetSpriteBatch(SpriteBatch spriteBatch);
    public void TextInput(char character);
}