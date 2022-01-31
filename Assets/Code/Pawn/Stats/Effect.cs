using ARPG.Enums;
using System;
using UnityEngine;

namespace ARPG.Pawns
{
    [Serializable]
    public struct Effect
    {
        public Effect(StatName stat, StatScore value, float duration)
        {
            this.statName = stat;
            this.stat = value;
            this.duration = duration;
        }

        /// <summary>
        /// The duration of the effect. If set to 0 the effect will be applied instantly
        /// </summary>
        [Range(0, 120)]
        [SerializeField] private float duration;
        public float Duration => duration;
        [SerializeField] private StatName statName;
        public StatName StatName => statName;
        [SerializeField] private StatScore stat;
        public StatScore Stat => stat;

        public float TickRate => stat.MaxValue / (duration * 3); // pseudo code
    }
}
