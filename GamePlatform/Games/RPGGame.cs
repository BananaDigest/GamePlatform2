
using System;

namespace GamePlatform
{
    public class RPGGame : Game
    {
        public int CurrentLevel { get; private set; } = 1; 
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
            Console.WriteLine("Installing RPG Game...");
        }

        public override void Launch(User user, PC pc)
        {
            if (!isControllerConnected)
            {
                Console.WriteLine("Для запуску гри в мультиплеєрному режимi потрiбен манiпулятор!");
                isMultiplayer = false;
            }

            Console.WriteLine("Запускається RPG Game...");
            Console.WriteLine($" Поточна платформа: {pc.Platform}");

            if (pc.Platform == Platform.Mobile)
            {
                int stream;
                Console.WriteLine("Бажаєте транслювати гру на iнший пристрiй?\n1) Так\n2) Нi");
                int.TryParse(Console.ReadLine(), out stream);
                if (stream == 1)
                {
                    mobileStream = new MobileStream(pc, user);
                    mobileStream.StreamingStarted += (device) =>
                Console.WriteLine($"[LOG] Трансляцiя гри почалася на {device}");//підписка
                    mobileStream.StreamToDevice();
                }
                else
                {
                    LoadProgress(user);
                    ConnectController();
                    SelectMode();
                    SelectCharacters();
                    StartSimulation();
                }
            }
            else
            {
                LoadProgress(user);
                ConnectController();
                SelectMode();
                SelectCharacters();
                StartSimulation();
            }
        }
        public void ConnectController()
        {
            Console.WriteLine("Хочете пiд'єднати контролер?\n1) Так\n2) Нi\n");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Некоректний вибiр.");
                return;
            }
            switch (choice)
            {
                case 1:
                    isControllerConnected = true;
                    Console.WriteLine("Манiпулятор пiдключено!");
                    break;
                case 2:
                    isControllerConnected = false;
                    break;
                default:
                    Console.WriteLine("Некоректний вибiр!");
                    break;
            }
            
        }

        private bool SelectMode()
        {
            if (isControllerConnected)
            {
                Console.WriteLine("Оберiть режим гри:");
                Console.WriteLine("1) Соло");
                Console.WriteLine("2) Мультиплеєр");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice == 2)
                {
                    isMultiplayer = true;
                }
            }
            return isMultiplayer;
        }

        private void SelectCharacters()
        {
            Console.WriteLine("Оберiть персонажа:");
            for (int i = 0; i < characters.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {characters[i]}");
            }

            player1 = characters[GetCharacterChoice()];
            if (isMultiplayer)
            {
                Console.WriteLine("Оберiть другого персонажа:");
                player2 = characters[GetCharacterChoice()];
            }
        }

        private int GetCharacterChoice()
        {
            int choice;
            do
            {
                Console.Write("Введiть номер персонажа: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > characters.Length);

            return choice - 1;
        }

        public override void StartSimulation()
        {
            if (!isMultiplayer)
            {
                Console.WriteLine("Гра RPG розпочалася!");
                bool playing = true;
                if (!isMultiplayer)
                {
                    while (playing)
                    {
                        Console.WriteLine($"Ви на {CurrentLevel} рiвнi.");
                        Console.WriteLine("Оберіть дiю:");
                        Console.WriteLine("1) Битися з монстром");
                        Console.WriteLine("2) Дослідити свiт");
                        Console.WriteLine("3) Вийти з гри");

                        if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine($"Ви перемогли монстра!");
                                CurrentLevel++;
                                break;
                            case 2:
                                Console.WriteLine("Ви знайшли скарби!");
                                break;
                            case 3:
                                playing = false;
                                Console.WriteLine($"Гра завершена. Ваш рiвень: {CurrentLevel}");
                                break;
                            default:
                                Console.WriteLine("Некоректний вибiр!");
                                break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Гра RPG розпочалася в режимi мультиплеєр!");
                bool playing = true;
                if (isMultiplayer)
                {
                    while (playing)
                    {
                        Console.WriteLine($"Ви на {CurrentLevel} рiвнi.");
                        Console.WriteLine("Оберiть дiю:");
                        Console.WriteLine("1) Битися з монстром");
                        Console.WriteLine("2) Дослідити свiт");
                        Console.WriteLine("3) Вийти з гри");
                        if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
                        switch (choice)
                        {
                            case 1:
                                string attacker = SelectAttacker(isMultiplayer);
                                Console.WriteLine($"{attacker} перемiг монстра!");
                                CurrentLevel++;
                                break;
                            case 2:
                                Console.WriteLine("Ви знайшли скарби!");
                                break;
                            case 3:
                                playing = false;
                                Console.WriteLine($"Гра завершена. Ваш рiвень: {CurrentLevel}");
                                break;
                            default:
                                Console.WriteLine("Некоректний вибiр!");
                                break;
                        }
                    }
                }
            }
        }

        private string SelectAttacker(bool isMultiplayer)
        {
            if (!isMultiplayer) return player1;

            Console.WriteLine("Хто буде дiяти?");
            Console.WriteLine($"1) {player1}");
            Console.WriteLine($"2) {player2}");

            int choice;
            do
            {
                Console.Write("Введiть номер: ");
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