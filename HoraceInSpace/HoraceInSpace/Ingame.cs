using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Ingame : AState
{
    private Horace _horace;
    public Ingame()
    {
        _horace = new Horace();
    }
    
    public override void Update(GameTime gameTime)
    {
        _horace.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _horace.Draw(spriteBatch);
    }

    public override void CheckInputs()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit = true;
        if (Keyboard.GetState().IsKeyDown(Keys.W))
            _horace.Forward();
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            _horace.Back();
    }

    public override StateEnum NewState()
    {
        throw new System.NotImplementedException();
    }
}