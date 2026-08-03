using System;
using System.Threading;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.States;

/// <summary>
/// ParallelWrapper runs on background and allows for the parallel update in a given class with an Update(GameTime) method.
/// Can be configured to either run on background or not with a constructor parameter.
/// </summary>
public sealed class ParallelWrapper : IDisposable
{
    private readonly Action<GameTime> _updateAction;
    private readonly Thread _thread;

    private readonly AutoResetEvent _updateSignal = new(false);
    private readonly Lock _lock = new();

    private volatile GameTime _gameTime;
    private volatile bool _running = true;

    /// <summary>
    /// Constructor that creates and starts its thread.
    /// </summary>
    /// <param name="updateAction">Update method, that is then called on signal.</param>
    /// <param name="isBackground">Sets the thread parameter IsBackground based on this parameter.</param>
    public ParallelWrapper(Action<GameTime> updateAction, bool isBackground)
    {
        _updateAction = updateAction;

        _thread = new Thread(UpdateLoop)
        {
            IsBackground = isBackground
        };

        _thread.Start();
    }

    /// <summary>
    /// Signals the Update method that was passed in the constructor.
    /// Saves the passed gameTime safely.
    /// </summary>
    /// <param name="gameTime"></param>
    public void TriggerUpdate(GameTime gameTime)
    {
        lock (_lock)
        {
            _gameTime = gameTime;
        }

        _updateSignal.Set();
    }

    /// <summary>
    /// Main loop of the thread.
    /// Waits for a signal and then calls the Update method passed in the constructor.
    /// Uses the last saved gameTime to pass to the Update method.
    /// </summary>
    private void UpdateLoop()
    {
        while (_running)
        {
            _updateSignal.WaitOne();

            if (!_running)
                break;

            GameTime gameTime;

            lock (_lock)
            {
                gameTime = _gameTime;
            }

            if (gameTime != null)
                _updateAction(gameTime);
        }
    }

    /// <summary>
    /// Disposes of the object, by stopping and joining the thread.
    /// Calls Dispose on AutoResetEvent.
    /// </summary>
    public void Dispose()
    {
        _running = false;
        _updateSignal.Set();

        _thread.Join();

        _updateSignal.Dispose();
    }
}