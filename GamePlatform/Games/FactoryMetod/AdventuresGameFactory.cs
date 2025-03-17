
namespace GamePlatform2
{
    public class AdventuresGameFactory : IGameFactory
    {
        public Game CreateGame() => new AdventuresGame();
    }
}
