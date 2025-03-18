
using System;

namespace GamePlatform2
{
    public class AdventuresGame : Game
    {
        private int level = 1;
        private MobileStream mobileStream;
        readonly Random random = new Random();
        readonly string saveKey = "Adventures Game";

        public AdventuresGame() : base("Adventures Game", Platform.Windows | Platform.Mobile | Platform.MacOS | Platform.Linux , 4, 2, 2)
        {
        }

        public override void Install(PC pc)
        {
            MenuDisplayer.ShowMessage("Installing Adventures Game...");
        }

        public override void Launch(User user, PC pc)
        {
            MenuDisplayer.ShowMessage("Launching Adventures Game...");
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
                    base.Run(user, pc);
                }
            }
            else
            {
                base.Run(user, pc);
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
                MenuDisplayer.ShowAdventuresGameMenu(level);

                if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

                switch (choice)
                {
                    case 1:
                        MenuDisplayer.ShowSuccess("Ви бiгаєте по свiту i дослiджуєте околицi!");
                        break;
                    case 2:
                        SolvePuzzle();
                        break;
                    case 3:
                        SearchForChests();
                        break;
                    case 4:
                        playing = false;
                        MenuDisplayer.ShowMessage($"Гра завершена. Ваш рiвень: {level}");
                        break;
                    default:
                        MenuDisplayer.ShowError("Некоректний вибiр!");
                        break;
                }
            }
        }

        private void SolvePuzzle()
        {
            MenuDisplayer.ShowMessage("Головоломка: Скiльки буде 2 + 2?");
            if (int.TryParse(Console.ReadLine(), out int answer) && answer == 4)
            {
                MenuDisplayer.ShowSuccess("Правильно! Ви отримали рiвень!");
                LevelUp();
            }
            else
            {
                MenuDisplayer.ShowError("Неправильна вiдповiдь!");
            }

            MenuDisplayer.ShowMessage("Головоломка: Яке слово читається однаково злiва направо i справа налiво? (приклад: око)");
            string response = Console.ReadLine()?.Trim().ToLower();
            if (response == "око" || response == "радар")
            {
                MenuDisplayer.ShowSuccess("Правильно! Ви отримали рiвень!");
                LevelUp();
            }
            else
            {
                MenuDisplayer.ShowMessage("Неправильна вiдповiдь!");
            }
        }

        private void SearchForChests()
        {
            int foundChests = random.Next(0, 3);
            if (foundChests > 0)
            {
                MenuDisplayer.ShowSuccess($"Ви знайшли {foundChests} скринi! Ваш рiвень пiдвищено.");
                for (int i = 0; i < foundChests; i++)
                {
                    LevelUp();
                }
            }
            else
            {
                MenuDisplayer.ShowMessage("Ви не знайшли жодної скринi цього разу.");
            }
        }

        private void LevelUp()
        {
            level++;
            MenuDisplayer.ShowMessage($"Ваш рiвень тепер: {level}");
        }
    }

}
