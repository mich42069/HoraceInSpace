using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HoraceInSpace.Assets;

/// <summary>
/// Plays given looped sound on Update with smooth falloff to eliminate sound anomallies.
/// </summary>
public class SoundWrapper
{
    private readonly SoundEffectInstance _soundEffectInstance;
    private float _volume;
    private const float FadeSpeed = 5f;

    /// <summary>
    /// Constructor to pass the given soundEffect with looped sound.
    /// </summary>
    /// <param name="soundEffectInstance">Given SoundEffect we want to loop.</param>
    public SoundWrapper(SoundEffectInstance soundEffectInstance)
    {
        _soundEffectInstance = soundEffectInstance;
        _soundEffectInstance.IsLooped = true;
    }

    /// <summary>
    /// Updates the sounds volume smoothly and accordingly.
    /// </summary>
    /// <param name="gameTime">Current GameTime.</param>
    /// <param name="active">If the sound is Active or Not - If it becomes deactivated the sound smoothly goes to 0,
    /// otherwise it is increasing its volume until at max.</param>
    public void UpdateSound(GameTime gameTime, bool active)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        float targetVolume = active ? 1f : 0f;

        _volume = MathHelper.Lerp(
            _volume,
            targetVolume,
            dt * FadeSpeed);

        if (active && _soundEffectInstance.State != SoundState.Playing)
        {
            _soundEffectInstance.Play();
        }

        _soundEffectInstance.Volume = _volume;

        if (!active && _volume < 0.01f)
        {
            _soundEffectInstance.Stop();
        }
    }

    /// <summary>
    /// Disposes of the sound instance.
    /// </summary>
    public void Dispose()
    {
        _soundEffectInstance.Dispose();
    }
}