using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public interface IStar
{
    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length);
    public void Draw(SpriteBatch spriteBatch);
    public void Update(GameTime gameTime);
}