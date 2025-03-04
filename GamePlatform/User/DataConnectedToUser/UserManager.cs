using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GamePlatform2
{
    public class UserManager
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private readonly string filePath = "usersData.json";

        public void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json) ?? new Dictionary<string, User>();
            }
        }

        public void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public User Login()
        {
            Console.WriteLine("Введiть iм'я користувача:");
            string username = Console.ReadLine();

            Console.WriteLine("Введіть пароль:");
            string password = Console.ReadLine();

            if (!users.ContainsKey(username))
            {
                var newUser = new User(username, password);
                users[username] = newUser;
                Console.WriteLine("Новий користувач створений.");
                return newUser;
            }

            if (users[username].Password != password)
            {
                Console.WriteLine("Невiрний пароль!");
                return null;
            }

            Console.WriteLine($"Користувач {username} увiйшов.");
            return users[username];
        }
    }
}
