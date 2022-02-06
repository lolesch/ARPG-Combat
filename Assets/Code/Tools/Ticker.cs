using System;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Tools
{
    [Serializable]
    /// <summary> A Ticker adds up it's progress to reach a predefined duration by a given tickDelta </summary>
    public class Ticker
    {
        public Ticker(float duration, bool isDelayed = false)
        {
            this.duration = duration;
            progress = isDelayed ? 0 : duration;
        }

        [SerializeField] private float duration;
        public float Duration => duration;

        [SerializeField] private float progress;
        public float Progress => progress;

        /// <summary> progress has not reached duration </summary>
        public bool IsTicking => progress < Duration;

        // public float Progress => Mathf.Clamp(progress, 0, duration);
        public float Progress01 => Mathf.Clamp01(progress / Duration);
        public float Remaining => Duration - progress;

        /// <summary> Increases the timers progress by tickInterval </summary>
        public float Tick(float tickInverval) => progress += tickInverval;

        /// <summary> Sets the current progress to 0 </summary>
        public void Restart() => progress = 0f;
    }

    public class DurationalTicker
    {
        // for the give duration each tickinterval this ticker calls an event that others can subscribe to
        public DurationalTicker(float duration, float tickInterval) { }


    }
}