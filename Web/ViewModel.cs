using DomainObjects;

namespace Web
{
    public class ViewModel
    {
        public string Version { get; internal set; }
        public User User { get; internal set; }
        public bool AccessGranted { get; internal set; }
    }
}