
using System;

namespace GamePlatform2
{
        public class UserProgressObserver : IObserver<GameProgressData>
        {
            public void OnNext(GameProgressData data)
            {
                MenuDisplayer.ShowMessage($"Прогрес у {data.GameName} оновлено: {data.Stat} {data.Value}");
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }
        }
}
