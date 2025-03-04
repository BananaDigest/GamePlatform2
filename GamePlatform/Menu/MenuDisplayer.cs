using System;
using System.Collections.Generic;

namespace GamePlatform2
{
    public static class MenuDisplayer
    {
        public static void ShowPCList(List<PC> pcs)
        {
            Console.WriteLine("Оберiть пристрiй:");
            for (int i = 0; i < pcs.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {pcs[i].Name}");
            }
        }

        public static void ShowGameList(List<string> games)
        {
            Console.WriteLine("Оберiть гру для запуску:");
            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {games[i]}");
            }
        }

        public static void ShowCharacterList(string[] characters)
        {
            Console.WriteLine("Оберiть персонажа:");
            for (int i = 0; i < characters.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {characters[i]}");
            }
        }
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void ShowSuccess(string successMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(successMessage);
            Console.ResetColor();
        }
        public static void ShowError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

        public static void ShowInstallerMenu()
        {
            Console.WriteLine("Оберiть гру для встановлення:");
            Console.WriteLine("1) Strategy Game");
            Console.WriteLine("2) RPG Game");
            Console.WriteLine("3) Adventures Game");
        }

        public static void ShowGameSimulationMenu()
        {
            Console.WriteLine("1) Встановити гру");
            Console.WriteLine("2) Запустити гру");
            Console.WriteLine("3) Вийти з акаунту");
            Console.WriteLine("4) Вийти з ПК");
            Console.WriteLine("5) Завершити програму");
        }

        public static void ShowStrategyGameMenu()
        {
            Console.WriteLine("Оберiть дiю:");
            Console.WriteLine("1) Побудувати вежу (вартiсть 20 монет)");
            Console.WriteLine("2) Напасти на ворогiв");
            Console.WriteLine("3) Вийти з гри");
        }

        public static void ShowMobileStreamMenu()
        {
            Console.WriteLine("Оберiть пристрiй для трансляцiї:");
            Console.WriteLine("1) Smart TV");
            Console.WriteLine("2) Комп’ютер");
            Console.WriteLine("3) Планшет");
        }

        public static void ShowRPGGameMenu(int CurrentLevel)
        {
            Console.WriteLine($"Ви на {CurrentLevel} рiвнi.");
            Console.WriteLine("Оберiть дiю:");
            Console.WriteLine("1) Битися з монстром");
            Console.WriteLine("2) Дослiдити свiт");
            Console.WriteLine("3) Вийти з гри");
        }

        public static void ShowAdventuresGameMenu(int level)
        {
            Console.WriteLine($"Ви на {level} рiвнi.");
            Console.WriteLine("Оберiть дiю:");
            Console.WriteLine("1) Бiгти по свiту");
            Console.WriteLine("2) Розв'язати головоломку");
            Console.WriteLine("3) Шукати скринi");
            Console.WriteLine("4) Вийти з гри");
        }

        public static void ShowRPGGameSelectModeMenu()
        {
            Console.WriteLine("Оберiть режим гри:");
            Console.WriteLine("1) Сiнгплеєр");
            Console.WriteLine("2) Мультиплеєр");
        }
    }
}
