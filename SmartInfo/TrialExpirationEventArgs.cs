using System;

namespace SmartInfo
{
    public class TrialExpirationEventArgs : EventArgs
    {
        public DateTime ExpirationDate { get; set; }
    }
}
