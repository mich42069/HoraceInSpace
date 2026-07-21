using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Ingame : AState
{
    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
    }

    public override void CheckInputs()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit = true;
    }

    public override StateEnum NewState()
    {
        throw new System.NotImplementedException();
    }
}