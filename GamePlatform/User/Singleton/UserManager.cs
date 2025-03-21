﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GamePlatform2
{
    public class UserManager
    {
        private static readonly Lazy<UserManager> instance = new Lazy<UserManager>(() => new UserManager());
        public static UserManager Instance => instance.Value;

        private Dictionary<string, User> users = new Dictionary<string, User>();
        private readonly string filePath = "usersData.json";

        private UserManager()
        {
            LoadUsers();
        }

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
            MenuDisplayer.ShowMessage("Введiть iм'я користувача:");
            string username = Console.ReadLine();

            MenuDisplayer.ShowMessage("Введiть пароль:");
            string password = Console.ReadLine();

            if (!users.ContainsKey(username))
            {
                var newUser = new User(username, password);
                users[username] = newUser;
                MenuDisplayer.ShowSuccess("Новий користувач створений.");
                return newUser;
            }

            if (users[username].Password != password)
            {
                MenuDisplayer.ShowError("Невiрний пароль!");
                return null;
            }

            MenuDisplayer.ShowSuccess($"Користувач {username} увiйшов.");
            return users[username];
        }
    }

}
