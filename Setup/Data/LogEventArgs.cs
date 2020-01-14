using System;

namespace Setup.Data
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string log)
        {
            Log = log;
        }

        public string Log { get; }
    }
}
