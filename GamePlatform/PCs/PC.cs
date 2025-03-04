using System.Collections.Generic;

namespace GamePlatform
{
    public class PC
    {
        public string Name { get; }
        public int RAM { get; set; }
        public int CPU { get; set; }
        public int GPU { get; set; }
        public int Storage { get; set; }
        public List<string> InstalledGames { get; } = new List<string>();
        public Platform Platform { get; set; }

        public PC(string name, Platform platform, int cpu, int ram, int gpu, int storage)
        {
            Name = name;
            Platform = platform;
            CPU = cpu;
            RAM = ram;
            GPU = gpu;
            Storage = storage;
        }
    }
}
