using System;
using System.Collections.Generic;
namespace GamePlatform2
{
    public class Unsubscriber : IDisposable
    {
        private List<IObserver<GameProgressData>> _observers;
        private IObserver<GameProgressData> _observer;

        public Unsubscriber(List<IObserver<GameProgressData>> observers, IObserver<GameProgressData> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
