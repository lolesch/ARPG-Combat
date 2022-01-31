using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Tools
{
    /// <summary>
    /// A Ticker adds up it's progress to reach a predefined duration by a given tickDelta
    /// </summary>
    public class Ticker
    {
        private float duration = 1f;
        private float progress = 0f;

        /// <summary>
        /// progress has not reached duration
        /// </summary>
        public bool IsTicking => progress < duration;

        public float Progress => Mathf.Clamp(progress, 0, duration);

        public float Progress01 => Mathf.Clamp01(progress / duration);

        public float Remaining => duration - progress;



        /// <summary>
        /// A Ticker adds up it's progress to reach a predefined duration by a given tickDelta
        /// </summary>
        public Ticker(float duration, bool isDelayed = false)
        {
            if (0 < duration)
            {
                this.duration = duration;
                progress = isDelayed ? 0 : duration;
            }
            else
                EditorDebug.LogWarning($"The Ticker's duration is {duration} - so it will never tick");
        }

        /// <summary>
        /// Increases the timers progress by tickInterval
        /// </summary>
        public float Tick(float tickInverval) => progress += tickInverval;

        /// <summary>
        /// Resets the current progress to 0
        /// </summary>
        public void Restart() => progress = 0f;
    }
}