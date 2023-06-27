using System;
using System.Collections.Generic;
using Utils;

namespace Managers
{
    public class TimerManager : SingletonManager<TimerManager>
    {
        private List<Timer> timers = new List<Timer>();

        private List<Timer> timersToAdd = new List<Timer>();

        public void RegisterTimer(Timer timer) => timersToAdd.Add(timer);
        public void CancelAllTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Cancel();
            }

            timers = new List<Timer>();
            timersToAdd = new List<Timer>();
        }

        public void PauseAllTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Pause();
            }
        }

        public void ResumeAllTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Resume();
            }
        }
        
        private void Update() => UpdateAllTimers();

        private void UpdateAllTimers()
        {
            if (timersToAdd.Count > 0)
            {
                timers.AddRange(timersToAdd);
                timersToAdd.Clear();
            }

            foreach (Timer timer in timers)
            {
                timer.Update();
            }

            timers.RemoveAll(timer => timer.IsDone);
        }
    }
}