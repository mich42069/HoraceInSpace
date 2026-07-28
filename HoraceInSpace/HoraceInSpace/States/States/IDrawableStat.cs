using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States;

public interface IDrawableStat
{
    public void Draw(SpriteBatch spriteBatch);
    public void Update(GameTime gameTime);
}