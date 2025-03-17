
namespace GamePlatform2
{
    public class StrategyGameFactory : IGameFactory
    {
        public Game CreateGame() => new StrategyGame();
    }
}
