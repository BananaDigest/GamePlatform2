using System;
using System.Threading;

namespace GamePlatform2
{
    public class MobileStream
    {
        private PC pc;
        private User user;
        public event Action<string> StreamingStarted; // Подія для старту трансляції
        public MobileStream(PC pc, User user)
        {
            this.pc = pc ?? throw new ArgumentNullException(nameof(pc));
            this.user = user ?? throw new ArgumentNullException(nameof(user));
        }
        public void StreamToDevice()
        {
            if (pc.Platform != Platform.Mobile)
            {
                MenuDisplayer.ShowMessage("Трансляцiя доступна тiльки з мобiльного пристрою!");
                return;
            }

            MenuDisplayer.ShowMobileStreamMenu();

            int choice;
            do
            {
                MenuDisplayer.ShowMessage("Введіть номер: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3);

            string device = choice == 1 ? "Smart TV" : choice == 2 ? "Комп’ютер" : "Планшет";

            StreamingStarted?.Invoke(device); // Виклик події


            while (!Console.KeyAvailable)
            {
                MenuDisplayer.ShowMessage($"Гра транслюється на {device}...");
                MenuDisplayer.ShowMessage("Натиснiть будь-яку кнопку, щоб зупинити трансляцiю");
                Thread.Sleep(2000);
            }
            Console.ReadKey(true);
            MenuDisplayer.ShowSuccess("Трансляцiю зупинено.");

        }
    }
}
