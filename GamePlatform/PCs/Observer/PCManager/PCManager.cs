﻿using System;
using System.Collections.Generic;

namespace GamePlatform2
{
    public class PCManager
    {
        public event EventHandler<PCChosenEventArgs> PCChosen;
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
                PC selectedPC = pcs[choice - 1];
                OnPCChosen(selectedPC.Name);
                return selectedPC;
            }
            MenuDisplayer.ShowError("Некоректний вибiр!");
            return SelectPC();
        }
        protected virtual void OnPCChosen(string pcName)
        {
            PCChosen?.Invoke(this, new PCChosenEventArgs(pcName));
        }
    }
}
