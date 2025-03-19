
namespace GamePlatform2
{
    public class GameProgressData
    {
        public string GameName { get; }
        public string Stat { get; }
        public int Value { get; }

        public GameProgressData(string gameName, string stat, int value)
        {
            GameName = gameName;
            Stat = stat;
            Value = value;
        }
    }
}
