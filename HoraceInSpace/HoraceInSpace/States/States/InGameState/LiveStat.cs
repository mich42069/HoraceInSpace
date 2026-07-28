using HoraceInSpace.Assets;
using HoraceInSpace.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States.InGameState;

public class LiveStat(Horace horace) : IDrawableStat
{
    private const int Margin = 10;
    private const int Spacing = 8;
    private const float Scale = 1.5f;
    private readonly Horace _horace = horace;
    private int _horaceLives;

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _horaceLives; i++)
        {
            Vector2 position = new(
                Margin + i * (Textures.Horace.Width * Scale + Spacing),
                Margin);

            spriteBatch.Draw(
                Textures.Horace,
                position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f);
        }
    }

    public void Update(GameTime gameTime)
    {
        _horaceLives = _horace.Lives;
    }
}