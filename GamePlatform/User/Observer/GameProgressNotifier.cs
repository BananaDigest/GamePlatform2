using System;
using System.Collections.Generic;

namespace GamePlatform2
{
    public class GameProgressNotifier : IObservable<GameProgressData>
    {
        private List<IObserver<GameProgressData>> observers = new List<IObserver<GameProgressData>>();

        public IDisposable Subscribe(IObserver<GameProgressData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        public void Notify(string game, string stat, int value)
        {
            var data = new GameProgressData(game, stat, value);
            foreach (var observer in observers)
            {
                observer.OnNext(data);
            }
        }
    }
}
