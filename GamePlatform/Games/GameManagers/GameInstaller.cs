using System;

namespace GamePlatform2
{
    public class GameInstaller
    {
        public void InstallGame(PC pc)
        {
            MenuDisplayer.ShowInstallerMenu();

            string[] gameNames = { "Strategy Game", "RPG Game", "Adventures Game" };
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
            {
                if (pc.Storage >= 50)
                {
                    pc.InstalledGames.Add(gameNames[choice - 1]);
                    pc.Storage -= 50;
                    MenuDisplayer.ShowSuccess($"{gameNames[choice - 1]} встановлено!");
                }
                else
                {
                    MenuDisplayer.ShowError("Недостатньо мiсця.");
                }
            }
            else
            {
                MenuDisplayer.ShowError("Некоректний вибiр.");
            }
        }
    }
}
