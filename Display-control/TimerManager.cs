using System;
using System.Timers;

namespace Display_control
{
    public class TimerManager
    {
        private static Timer _timer;
        public void Subscribe(Action action)
        {
            var timer = GetTimer();
            timer.Elapsed += (sender,e)=>action.Invoke();
        }

        private Timer GetTimer()
        {
            if (_timer == null)
            {
                _timer = new Timer()
                {
                    AutoReset = true,
                    Interval = 500,
                    Enabled = true
                };
            }
            return _timer;
        }
    }
}
