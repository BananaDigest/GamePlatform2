using System;

namespace GamePlatform2
{
    public class GameInstaller
    {
        public void InstallGame(PC pc)
        {
            Console.WriteLine("Оберiть гру для встановлення:");
            Console.WriteLine("1) Strategy Game");
            Console.WriteLine("2) RPG Game");
            Console.WriteLine("3) Adventures Game");

            string[] gameNames = { "Strategy Game", "RPG Game", "Adventures Game" };
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
            {
                if (pc.Storage >= 50)
                {
                    pc.InstalledGames.Add(gameNames[choice - 1]);
                    pc.Storage -= 50;
                    Console.WriteLine($"{gameNames[choice - 1]} встановлено!");
                }
                else
                {
                    Console.WriteLine("Недостатньо мiсця.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний вибiр.");
            }
        }
    }
}
