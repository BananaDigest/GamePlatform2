using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GamePlatform2
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; private set; }
        public Dictionary<string, Dictionary<string, int>> GameProgress { get; set; } = new Dictionary<string, Dictionary<string, int>>();
        private const string SaveFile = "usersData.json";

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            GameProgress = new Dictionary<string, Dictionary<string, int>>();
        }

        public void Login()
        {
            IsLoggedIn = true;
            Console.WriteLine($"{Username} logged in.");
        }

        public void Logout()
        {
            IsLoggedIn = false;
            Console.WriteLine($"{Username} logged out.");
        }

        public string LoadGame(string gameName)
        {
            return GameProgress.ContainsKey(gameName) ? GameProgress[gameName].ToString() : "No saved game.";
        }
        public void SaveProgress(string gameName, string stat, int value)
        {
            if (!GameProgress.ContainsKey(gameName))
            {
                GameProgress[gameName] = new Dictionary<string, int>();
            }

            GameProgress[gameName][stat] = value;

            try
            {
                string json = JsonConvert.SerializeObject(GameProgress, Formatting.Indented);
                File.WriteAllText(SaveFile, json);
                Console.WriteLine($"{gameName} - {stat} saved: {value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження прогресу: {ex.Message}");
            }
        }

        public int LoadProgress(string gameName, string stat)
        {
            LoadAllProgress();

            if (GameProgress.TryGetValue(gameName, out var gameStats) &&
                gameStats.TryGetValue(stat, out int value))
            {
                return value;
            }

            Console.WriteLine($"[Warning] Данi для {gameName} не знайдено або пошкодженi. Використовується значення за замовчуванням.");
            return 1; 
        }

        private void LoadAllProgress()
        {
            if (!File.Exists(SaveFile)) return;

            try
            {
                string json = File.ReadAllText(SaveFile);
                var loadedData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(json);

                if (loadedData != null)
                {
                    GameProgress = new Dictionary<string, Dictionary<string, int>>();

                    foreach (var gameEntry in loadedData)
                    {
                        if (gameEntry.Value is Dictionary<string, int> validStats)
                        {
                            GameProgress[gameEntry.Key] = validStats;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("");
            }
        }
    }
}
