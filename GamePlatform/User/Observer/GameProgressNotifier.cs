using System.Collections.Generic;

namespace GamePlatform2
{
    public class GameProgressNotifier
    {
        private List<IGameProgressObserver> observers = new List<IGameProgressObserver>();

        public void Subscribe(IGameProgressObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IGameProgressObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string game, string stat, int value)
        {
            foreach (var observer in observers)
            {
                observer.OnProgressChanged(game, stat, value);
            }
        }
    }
}
