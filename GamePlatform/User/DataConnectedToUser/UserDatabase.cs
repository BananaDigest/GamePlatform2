using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GamePlatform
{
    public static class UserDatabase
    {
        private static string filePath = "users.json";
        public static void SaveUsers(Dictionary<string, User> users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine(" Данi користувачiв збережено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Помилка збереження: {ex.Message}");
            }
        }
        public static Dictionary<string, User> LoadUsers()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(" Файл користувачiв не знайдено. Створюється новий.");
                return new Dictionary<string, User>();
            }

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Dictionary<string, User>>(json) ?? new Dictionary<string, User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Помилка завантаження: {ex.Message}");
                return new Dictionary<string, User>();
            }
        }
    }

}
