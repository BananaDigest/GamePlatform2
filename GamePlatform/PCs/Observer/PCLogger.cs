using System;

namespace GamePlatform2
{
    public class PCLogger
    {
        public void Subscribe(PCManager selector)
        {
            selector.PCChosen += HandlePCChosen;
        }

        private void HandlePCChosen(object sender, PCChosenEventArgs e)
        {
            Console.WriteLine($"Було обрано ПК: {e.PCName}");
        }
    }

}
