using System;

namespace Setup.Data
{
    public class ProcessDataEventArgs : EventArgs
    {
        public ProcessDataEventArgs(string data)
        {
            Data = data;
        }

        public string Data { get; }
    }
}
