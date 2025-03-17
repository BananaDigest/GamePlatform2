
namespace GamePlatform2
{
    public class RPGGameFactory : IGameFactory
    {
        public Game CreateGame() => new RPGGame();
    }
}
