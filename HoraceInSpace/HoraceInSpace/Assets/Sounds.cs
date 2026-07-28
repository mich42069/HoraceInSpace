using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace HoraceInSpace.Assets;

public static class Sounds
{
    public static SoundEffect Shoot;
    public static SoundEffect Thruster;
    public static SoundEffect Explosion;
    public static SoundEffect HoraceGetsHit;

    public static void LoadContent(ContentManager content)
    {
        Shoot = content.Load<SoundEffect>("shooting");
        Thruster = content.Load<SoundEffect>("thruster_sound");
        Explosion =  content.Load<SoundEffect>("explosion");
        HoraceGetsHit = content.Load<SoundEffect>("scream");
    }
}