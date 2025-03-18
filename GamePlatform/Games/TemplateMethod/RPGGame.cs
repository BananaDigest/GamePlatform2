
using System;

namespace GamePlatform2
{
    public class RPGGame : Game
    {
        public int CurrentLevel { get; protected set; } = 1; 
        private bool isMultiplayer = false;
        private bool isControllerConnected = false;
        private MobileStream mobileStream;
        private string player1;
        private string player2;
        private readonly string[] characters = { "Eve", "Chloe", "Kim", "Jordan" };

        public RPGGame() : base("RPG Game", Platform.Windows, 8, 3, 4)
        {
        }

        public override bool CanRun(PC pc, User user)
        {
            return pc.RAM >= RequiredRAM && pc.CPU >= RequiredCPU && pc.GPU >= RequiredGPU;
        }

        public override void Install(PC pc)
        {
            MenuDisplayer.ShowMessage("Installing RPG Game...");
        }

        public override void Launch(User user, PC pc)
        {
            if (!isControllerConnected)
            {
                MenuDisplayer.ShowError("Для запуску гри в мультиплеєрному режимi потрiбен манiпулятор!");
                isMultiplayer = false;
            }

            MenuDisplayer.ShowMessage("Запускається RPG Game...");

            if (pc.Platform == Platform.Mobile)
            {
                MenuDisplayer.ShowMessage("Бажаєте транслювати гру на iнший пристрiй?\n1) Так\n2) Нi");
                int.TryParse(Console.ReadLine(), out int stream);
                if (stream == 1)
                {
                    mobileStream = new MobileStream(pc, user);
                    mobileStream.StreamingStarted += (device) =>
                Console.WriteLine($"[LOG] Трансляцiя гри почалася на {device}");//підписка
                    mobileStream.StreamToDevice();
                }
                else
                {
                    Run(user, pc);
                }
            }
            else
            {
                Run(user, pc);
            }
        }

        public override void Run(User user, PC pc)
        {
            LoadProgress(user);
            ConnectController();
            SelectMode();
            SelectCharacters();
            StartSimulation();
            SaveProgress(user);
        }
        public void ConnectController()
        {
            MenuDisplayer.ShowMessage("Хочете пiд'єднати контролер?\n1) Так\n2) Нi\n");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Некоректний вибiр.");
                return;
            }
            switch (choice)
            {
                case 1:
                    isControllerConnected = true;
                    MenuDisplayer.ShowSuccess("Манiпулятор пiдключено!");
                    break;
                case 2:
                    isControllerConnected = false;
                    break;
                default:
                    MenuDisplayer.ShowError("Некоректний вибiр!");
                    break;
            }
            
        }

        private bool SelectMode()
        {
            if (isControllerConnected)
            {
                MenuDisplayer.ShowRPGGameSelectModeMenu();

                if (int.TryParse(Console.ReadLine(), out int choice) && choice == 2)
                {
                    isMultiplayer = true;
                }
            }
            return isMultiplayer;
        }

        private void SelectCharacters()
        {
            MenuDisplayer.ShowCharacterList(characters);

            player1 = characters[GetCharacterChoice()];
            if (isMultiplayer)
            {
                MenuDisplayer.ShowMessage("Оберiть другого персонажа:");
                player2 = characters[GetCharacterChoice()];
            }
        }

        private int GetCharacterChoice()
        {
            int choice;
            do
            {
                MenuDisplayer.ShowMessage("Введiть номер персонажа: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > characters.Length);

            return choice - 1;
        }

        public override void StartSimulation()
        {
            if (!isMultiplayer)
            {
                MenuDisplayer.ShowMessage("Гра RPG розпочалася!");
                bool playing = true;
                if (!isMultiplayer)
                {
                    while (playing)
                    {
                        MenuDisplayer.ShowRPGGameMenu(CurrentLevel);

                        if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
                        switch (choice)
                        {
                            case 1:
                                MenuDisplayer.ShowSuccess($"Ви перемогли монстра!");
                                CurrentLevel++;
                                NotifyProgress("Рівень підвищено до ", CurrentLevel);
                                break;
                            case 2:
                                MenuDisplayer.ShowSuccess("Ви знайшли скарби!");
                                break;
                            case 3:
                                playing = false;
                                MenuDisplayer.ShowMessage($"Гра завершена. Ваш рiвень: {CurrentLevel}");
                                break;
                            default:
                                MenuDisplayer.ShowError("Некоректний вибiр!");
                                break;
                        }
                    }
                }
            }
            else
            {
                MenuDisplayer.ShowSuccess("Гра RPG розпочалася в режимi мультиплеєр!");
                bool playing = true;
                if (isMultiplayer)
                {
                    while (playing)
                    {
                        MenuDisplayer.ShowRPGGameMenu(CurrentLevel);
                        if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
                        switch (choice)
                        {
                            case 1:
                                string attacker = SelectAttacker(isMultiplayer);
                                MenuDisplayer.ShowSuccess($"{attacker} перемiг монстра!");
                                CurrentLevel++;
                                NotifyProgress("Рiвень пiдвищено до ", CurrentLevel);
                                break;
                            case 2:
                                MenuDisplayer.ShowSuccess("Ви знайшли скарби!");
                                break;
                            case 3:
                                playing = false;
                                MenuDisplayer.ShowMessage($"Гра завершена. Ваш рiвень: {CurrentLevel}");
                                break;
                            default:
                                MenuDisplayer.ShowError("Некоректний вибiр!");
                                break;
                        }
                    }
                }
            }
        }

        private string SelectAttacker(bool isMultiplayer)
        {
            if (!isMultiplayer) return player1;

            MenuDisplayer.ShowMessage("Хто буде дiяти?");
            MenuDisplayer.ShowMessage($"1) {player1}");
            MenuDisplayer.ShowMessage($"2) {player2}");

            int choice;
            do
            {
                MenuDisplayer.ShowMessage("Введiть номер: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2));

            return choice == 1 ? player1 : player2;
        }

        public override void SaveProgress(User user)
        {
            user.SaveProgress("RPG Game", "Level", CurrentLevel);
        }

        public override void LoadProgress(User user)
        {
            CurrentLevel = user.LoadProgress("RPG Game", "Level");
        }
    }
}