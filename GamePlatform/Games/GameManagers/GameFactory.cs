using System;

namespace GamePlatform2
{
    public static class GameFactory
    {
        public static Game CreateGame(string gameName)
        {
            switch (gameName)
            {
                case "Strategy Game": return new StrategyGame();
                case "RPG Game": return new RPGGame();
                case "Adventures Game": return new AdventuresGame();
                default: throw new ArgumentException("Невідома гра");
            }
        }
    }
}
