using System;
using System.Threading;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Context;

public sealed class ParallelWrapper : IDisposable
{
    private readonly Action<GameTime> _updateAction;
    private readonly Thread _thread;

    private readonly AutoResetEvent _updateSignal = new(false);
    private readonly Lock _lock = new();

    private volatile GameTime _gameTime;
    private volatile bool _running = true;

    public ParallelWrapper(Action<GameTime> updateAction)
    {
        _updateAction = updateAction;

        _thread = new Thread(UpdateLoop)
        {
            IsBackground = true
        };

        _thread.Start();
    }

    public void TriggerUpdate(GameTime gameTime)
    {
        lock (_lock)
        {
            _gameTime = gameTime;
        }

        _updateSignal.Set();
    }

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

    public void Dispose()
    {
        _running = false;
        _updateSignal.Set();

        _thread.Join();

        _updateSignal.Dispose();
    }
}