
namespace GamePlatform2
{
    public class UserProgressNotifier : IGameProgressObserver
    {
        public void OnProgressChanged(string game, string stat, int value)
        {
            MenuDisplayer.ShowMessage($" Прогрес у {game} оновлено: {stat} {value}");
        }
    }
}
