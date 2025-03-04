using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform
{
    public class GameLauncher
    {
        public void LaunchGame(PC pc, User user)
        {
            if (pc.InstalledGames.Count == 0)
            {
                Console.WriteLine("Немає встановлених iгор.");
                return;
            }

            Console.WriteLine("Оберiть гру для запуску:");
            for (int i = 0; i < pc.InstalledGames.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {pc.InstalledGames[i]}");
            }

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
                    Console.WriteLine("Недостатньо ресурсiв для запуску гри.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний вибiр.");
            }
        }
    }
}
