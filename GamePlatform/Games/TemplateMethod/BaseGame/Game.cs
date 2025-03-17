
namespace GamePlatform2
{
    public abstract class Game
    {
        public string Name { get; }
        public Platform Platform { get; }
        public int RequiredRAM { get; }
        public int RequiredCPU { get; }
        public int RequiredGPU { get; }
        public abstract void Install(PC pc);
        public abstract void Launch(User user, PC pc);
        public abstract void SaveProgress(User user);
        public abstract void LoadProgress(User user);
        
        protected Game(string name, Platform platform, int requiredRAM, int requiredCPU, int requiredGPU)
        {
            Name = name;
            Platform = platform;
            RequiredRAM = requiredRAM;
            RequiredCPU = requiredCPU;
            RequiredGPU = requiredGPU;
        }

        public abstract bool CanRun(PC pc, User user);
        public abstract void StartSimulation();

        public void Run(User user, PC pc)
        {
            Install(pc);
            LoadProgress(user);

            if (!CanRun(pc, user))
            {
                MenuDisplayer.ShowError("Недостатньо ресурсів для запуску гри.");
                return;
            }

            Launch(user, pc);
            StartSimulation();
            SaveProgress(user);
        }
    }

}
