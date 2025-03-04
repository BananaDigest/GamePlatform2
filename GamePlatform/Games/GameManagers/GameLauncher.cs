using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform2
{
    public class GameLauncher
    {
        public void LaunchGame(PC pc, User user)
        {
            if (pc.InstalledGames.Count == 0)
            {
                MenuDisplayer.ShowMessage("Немає встановлених iгор.");
                return;
            }

            MenuDisplayer.ShowGameList(pc.InstalledGames);

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= pc.InstalledGames.Count)
            {
                string gameName = pc.InstalledGames[choice - 1];
                Game game = GameFactory.CreateGame(gameName);
                game.LoadProgress(user);

                if (game.CanRun(pc, user))
                {
                    game.Launch(user, pc);
                    game.SaveProgress(user);
                }
                else
                {
                    MenuDisplayer.ShowMessage("Недостатньо ресурсiв для запуску гри.");
                }
            }
            else
            {
                MenuDisplayer.ShowError("Некоректний вибiр.");
            }
        }
    }
}
