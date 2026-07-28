using HoraceInSpace.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HoraceInSpace.Entity.Hitbox;

public class ThrusterSoundWrapper
{
    private readonly SoundEffectInstance _thrusterSoundInstance;
    private float _thrusterVolume;
    private const float FadeSpeed = 5f;

    public ThrusterSoundWrapper(SoundEffectInstance thrusterSoundInstance)
    {
        _thrusterSoundInstance = thrusterSoundInstance;
        _thrusterSoundInstance.IsLooped = true;
    }

    public void UpdateThrusterSound(GameTime gameTime, bool active)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        float targetVolume = active ? 1f : 0f;

        _thrusterVolume = MathHelper.Lerp(
            _thrusterVolume,
            targetVolume,
            dt * FadeSpeed);

        if (active && _thrusterSoundInstance.State != SoundState.Playing)
        {
            _thrusterSoundInstance.Play();
        }

        _thrusterSoundInstance.Volume = _thrusterVolume;

        if (!active && _thrusterVolume < 0.01f)
        {
            _thrusterSoundInstance.Stop();
        }
    }
}