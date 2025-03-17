using System;
using System.Collections.Generic;

namespace GamePlatform2
{
    public class PCManager
    {
        private List<PC> pcs = new List<PC>
        {
            new PC("Windows PC", Platform.Windows, 8, 16, 6, 500),
            new PC("MacOS PC", Platform.MacOS, 6, 8, 4, 256),
            new PC("Linux PC", Platform.Linux, 3, 4, 2, 128),
            new PC("Mobile", Platform.Mobile, 4, 4, 2, 64)
        };

        public PC SelectPC()
        {
            MenuDisplayer.ShowPCList(pcs);

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= pcs.Count)
            {
                return pcs[choice - 1];
            }

            MenuDisplayer.ShowError("Некоректний вибір!");
            return SelectPC();
        }
    }
}
