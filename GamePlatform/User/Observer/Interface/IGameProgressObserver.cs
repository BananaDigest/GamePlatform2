
namespace GamePlatform2
{
    public interface IGameProgressObserver
    {
        void OnProgressChanged(string game, string stat, int value);
    }
}
