using System;

namespace GamePlatform2
{
    public class PCChosenEventArgs : EventArgs
    {
        public string PCName { get; }
        public PCChosenEventArgs(string pcName) => PCName = pcName;
    }
}
