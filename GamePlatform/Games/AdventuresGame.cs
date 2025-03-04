
using System;

namespace GamePlatform
{
    public class AdventuresGame : Game
    {
        private int level = 1;
        private MobileStream mobileStream;
        private Random random = new Random();
        private string saveKey = "Adventures Game";

        public AdventuresGame() : base("Adventures Game", Platform.Windows | Platform.Mobile | Platform.MacOS | Platform.Linux , 4, 2, 2)
        {
        }

        public override void Install(PC pc)
        {
            Console.WriteLine("Installing Adventures Game...");
        }

        public override void Launch(User user, PC pc)
        {
            Console.WriteLine("Launching Adventures Game...");
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
                    StartSimulation();
                }
            }
            else
            {
                LoadProgress(user);
                StartSimulation();
            }
        }

        public override void SaveProgress(User user)
        {
            user.SaveProgress(saveKey, "Level", level);
        }

        public override void LoadProgress(User user)
        {
            level = user.LoadProgress(saveKey, "Level");
        }

        public override bool CanRun(PC pc, User user)
        {
            return (pc.Platform==Platform.Windows|| pc.Platform==Platform.MacOS|| pc.Platform == Platform.Linux || pc.Platform == Platform.Mobile) && pc.RAM >= RequiredRAM && pc.CPU >= RequiredCPU && pc.GPU >= RequiredGPU;
        }

        public override void StartSimulation()
        {
            bool playing = true;
            while (playing)
            {
                Console.WriteLine($"Ви на {level} рiвнi.");
                Console.WriteLine("Оберiть дiю:");
                Console.WriteLine("1) Бiгти по свiту");
                Console.WriteLine("2) Розв'язати головоломку");
                Console.WriteLine("3) Шукати скринi");
                Console.WriteLine("4) Вийти з гри");

                if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Ви бiгаєте по свiту i дослiджуєте околицi!");
                        break;
                    case 2:
                        SolvePuzzle();
                        break;
                    case 3:
                        SearchForChests();
                        break;
                    case 4:
                        playing = false;
                        Console.WriteLine($"Гра завершена. Ваш рiвень: {level}");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибiр!");
                        break;
                }
            }
        }

        private void SolvePuzzle()
        {
            Console.WriteLine("Головоломка: Скiльки буде 2 + 2?");
            if (int.TryParse(Console.ReadLine(), out int answer) && answer == 4)
            {
                Console.WriteLine("Правильно! Ви отримали рiвень!");
                LevelUp();
            }
            else
            {
                Console.WriteLine("Неправильна відповідь!");
            }

            Console.WriteLine("Головоломка: Яке слово читається однаково злiва направо i справа налiво? (приклад: око)");
            string response = Console.ReadLine()?.Trim().ToLower();
            if (response == "око" || response == "радар")
            {
                Console.WriteLine("Правильно! Ви отримали рiвень!");
                LevelUp();
            }
            else
            {
                Console.WriteLine("Неправильна вiдповiдь!");
            }
        }

        private void SearchForChests()
        {
            int foundChests = random.Next(0, 3);
            if (foundChests > 0)
            {
                Console.WriteLine($"Ви знайшли {foundChests} скринi! Ваш рiвень пiдвищено.");
                for (int i = 0; i < foundChests; i++)
                {
                    LevelUp();
                }
            }
            else
            {
                Console.WriteLine("Ви не знайшли жодної скринi цього разу.");
            }
        }

        private void LevelUp()
        {
            level++;
            Console.WriteLine($"Ваш рiвень тепер: {level}");
        }
    }

}
