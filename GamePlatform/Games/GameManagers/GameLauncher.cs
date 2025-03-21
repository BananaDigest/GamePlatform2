﻿using System;
using System.Collections.Generic;

namespace GamePlatform2
{
    public class GameLauncher
    {
        private readonly Dictionary<string, IGameFactory> gameFactories = new Dictionary<string, IGameFactory>
        {
            { "Strategy Game", new StrategyGameFactory() },
            { "RPG Game", new RPGGameFactory() },
            { "Adventures Game", new AdventuresGameFactory() }
        };

        public void LaunchGame(PC pc, User user)
        {
            if (pc.InstalledGames.Count == 0)
            {
                MenuDisplayer.ShowMessage("Немає встановлених iгор.");
                return;
            }

            MenuDisplayer.ShowGameList(pc.InstalledGames);

            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice >= 1 && choice <= pc.InstalledGames.Count)
            {
                string gameName = pc.InstalledGames[choice - 1];

                if (gameFactories.TryGetValue(gameName, out IGameFactory factory))
                {
                    Game game = factory.CreateGame();
                    UserProgressObserver notifier = new UserProgressObserver();
                    IDisposable subscription = game.AttachProgressObserver(notifier);

                    game.LoadProgress(user);

                    if (game.CanRun(pc, user))
                    {
                        game.AttachProgressObserver(notifier);
                        game.Launch(user, pc);
                        game.DetachProgressObserver(subscription);
                    }
                    else
                    {
                        MenuDisplayer.ShowMessage("Недостатньо ресурсiв для запуску гри.");
                    }
                }
            }
            else
            {
                MenuDisplayer.ShowError("Некоректний вибiр.");
            }
        }
    }
}
