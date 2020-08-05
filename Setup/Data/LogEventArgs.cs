using System;

namespace Setup.Data
{
    internal class LogEventArgs : EventArgs
    {
        public LogEventArgs(string log)
        {
            Log = log;
        }

        public string Log { get; }
    }
}
