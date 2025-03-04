using System;
using System.Threading;

namespace GamePlatform2
{
    public class StrategyGame : Game
    {
        public int Towers { get; private set; } = 0;
        public int Money { get; private set; } = 1000;
        private Random random = new Random();
        private bool running = true;
        private PC currentPC;
        private User user;
        public StrategyGame() : base("Strategy Game", Platform.Windows, 6, 3, 6) { }

        public StrategyGame(PC currentPC, User user) : base("Strategy Game", Platform.Windows, 6, 3, 6)
        {
            this.currentPC = currentPC;
            this.user = user;
        }

        public override bool CanRun(PC currentPC, User user)
        {
            if (currentPC.Platform != Platform.Windows)
            {
                Console.WriteLine("Гра пiдтримується тiльки на Windows!");
                return false;
            }
            return currentPC.RAM >= RequiredRAM && currentPC.CPU >= RequiredCPU && currentPC.GPU >= RequiredGPU;
        }
        public override void Install(PC pc)
        {
            if (pc.Platform != Platform.Windows)
            {
                Console.WriteLine("Strategy games can only be installed on Windows.");
                return;
            }
            else
            {
                Console.WriteLine("Installing Strategy Game...");
            }
        }
        public override void Launch(User user, PC pc)
        {
            if (pc.Platform != Platform.Windows)
            {
                Console.WriteLine("Strategy games can only be played on Windows.");
                return;
            }
            else
            {
                Console.WriteLine("Launching Strategy Game...");
                LoadProgress(user);
                StartSimulation();
            }
        }

        public override void StartSimulation()
        {
            Console.WriteLine("Гра розпочалася!");
            Thread enemyThread = new Thread(SimulateEnemyAttacks);
            enemyThread.Start();

            while (running)
            {
                Console.WriteLine("Оберiть дiю:");
                Console.WriteLine("1) Побудувати вежу (вартiсть 20 монет)");
                Console.WriteLine("2) Напасти на ворогiв");
                Console.WriteLine("3) Вийти з гри");

                if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

                switch (choice)
                {
                    case 1:
                        BuildTower();
                        break;
                    case 2:
                        AttackEnemies();
                        break;
                    case 3:
                        running = false;
                        Console.WriteLine($"Гра завершена. Збережено {Towers} веж.");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір!");
                        break;
                }
            }
        }

        private void BuildTower()
        {
            if (Money >= 20)
            {
                Money -= 20;
                Towers++;
                Console.WriteLine($"Побудовано вежу! Всього веж: {Towers}, Залишок грошей: {Money}");
            }
            else
            {
                Console.WriteLine("Недостатньо грошей!");
            }
        }

        private void AttackEnemies()
        {
            int outcome = random.Next(3);
            if (outcome == 0)
            {
                int lostTowers = random.Next(1, Math.Max(2, Towers + 1));
                Towers = Math.Max(0, Towers - lostTowers);
                int lostMoney = random.Next(10, 51);
                Money = Math.Max(0, Money - lostMoney);
                Console.WriteLine($"Атака провалена! Втрачено {lostTowers} веж, {lostMoney} монет.");
            }
            else
            {
                int earnedMoney = random.Next(20, 101);
                int capturedTowers = random.Next(0, 11);
                Money += earnedMoney;
                Towers += capturedTowers;
                Console.WriteLine($"Атака вдала! Зароблено {earnedMoney} монет. Захоплено веж {capturedTowers}");
            }
        }

        private void SimulateEnemyAttacks()
        {
            while (running)
            {
                Thread.Sleep(random.Next(5000, 10000));
                if (!running) break;

                int lostTowers = random.Next(1, Math.Max(2, Towers + 1));
                Towers = Math.Max(0, Towers - lostTowers);
                int lostMoney = random.Next(10, 51);
                Money = Math.Max(0, Money - lostMoney);
                Console.WriteLine($"Ворог напав! Втрачено {lostTowers} веж, {lostMoney} монет.");
            }
        }
        public void EarnFunds(int amount, User user)
        {
            Money += amount;
            SaveProgress(user);
        }
        public override void SaveProgress(User user)
        { 
            user.SaveProgress("Strategy Game", "Money", Money);
            user.SaveProgress("Strategy Game", "Towers", Towers);
        }

        public override void LoadProgress(User user)
        {
            Money = user.LoadProgress("Strategy Game", "Money");
            Towers = user.LoadProgress("Strategy Game", "Towers");
        }

    }
}

