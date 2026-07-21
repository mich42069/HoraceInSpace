using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public interface IContextMachine
{
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
    public bool CanExit();
    public void SetSpriteBatch(SpriteBatch spriteBatch);
}