using System;

namespace GamePlatform2
{
    public class GameSimulation
    {
        private readonly PCManager pcManager = new PCManager();
        private readonly UserManager userManager = new UserManager();
        private readonly GameInstaller gameInstaller = new GameInstaller();
        private readonly GameLauncher gameLauncher = new GameLauncher();
        private string selectedDevice;

        public void Run()
        {
            userManager.LoadUsers();
            pcManager.ChosenPC += platform =>
            {
                selectedDevice = platform;
                Console.WriteLine($"Було обрано {platform}");
            };

            while (true)
            {
                PC pc = pcManager.SelectPC();
                User user = null;

                while (user == null)
                {
                    user = userManager.Login();
                }

                bool exitPCMenu = false;
                while (!exitPCMenu)
                {
                    Console.WriteLine("1) Встановити гру");
                    Console.WriteLine("2) Запустити гру");
                    Console.WriteLine("3) Вийти з акаунту");
                    Console.WriteLine("4) Вийти з ПК");
                    Console.WriteLine("5) Завершити програму");

                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            gameInstaller.InstallGame(pc);
                            break;
                        case "2":
                            gameLauncher.LaunchGame(pc, user);
                            userManager.SaveUsers();
                            break;
                        case "3":
                            user = null;
                            while (user == null)
                            {
                                user = userManager.Login();
                            }
                            break;
                        case "4":
                            exitPCMenu = true;
                            break;
                        case "5":
                            userManager.SaveUsers();
                            return;
                        default:
                            Console.WriteLine("Некоректний вибiр.");
                            break;
                    }
                }
            }
        }
    }
}
