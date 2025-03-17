using System;
namespace GamePlatform2
{
    public class GameSimulation
    {
        private readonly PCManager pcManager = new PCManager();
        private readonly UserManager userManager = UserManager.Instance;
        private readonly GameInstaller gameInstaller = new GameInstaller();
        private readonly GameLauncher gameLauncher = new GameLauncher();

        public void Run()
        {

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
                    MenuDisplayer.ShowGameSimulationMenu();

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
                            MenuDisplayer.ShowError("Некоректний вибiр.");
                            break;
                    }
                }
            }
        }
    }
}
