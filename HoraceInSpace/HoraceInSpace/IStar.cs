using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public interface IStar
{
    public void Flicker(GameTime gameTime, double intensity);
    public void Draw(SpriteBatch spriteBatch);

    public void Update(GameTime gameTime);
}