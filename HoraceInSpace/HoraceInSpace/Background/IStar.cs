using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Background;

public interface IStar
{
    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length);
    public void Draw(SpriteBatch spriteBatch);
    public void Update(GameTime gameTime);
}