using System;

namespace Setup.Data
{
    internal class ProcessDataEventArgs : EventArgs
    {
        public ProcessDataEventArgs(string data)
        {
            Data = data;
        }

        public string Data { get; }
    }
}
