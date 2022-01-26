using System;
using System.Collections.Generic;

namespace ARPG.Tools
{
    public class StepLock
    {
        private readonly List<object> lockers = new List<object>();
        public bool Unlocked => 0 == lockers.Count;

        /// <summary>
        /// This action is called on toggle - on first in it invokes true, on last out it invokes false
        /// </summary>
        public event Action<bool> locked;

        public virtual void Add(object locker)
        {
            if (Unlocked)
                locked?.Invoke(true);
            if (!lockers.Contains(locker))
                lockers.Add(locker);
        }

        public virtual void Remove(object locker)
        {
            if (lockers.Contains(locker))
                lockers.Remove(locker);
            if (Unlocked)
                locked?.Invoke(false);
        }

        public void ForceUnlock() => lockers.Clear();
    }
}