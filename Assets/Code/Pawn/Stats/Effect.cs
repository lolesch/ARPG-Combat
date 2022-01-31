using ARPG.Enums;
using System;
using UnityEngine;

namespace ARPG.Pawns
{
    [Serializable]
    public struct Effect
    {
        public Effect(StatName stat, StatScore value, float duration, float tickrate = 0.2f)
        {
            this.statName = stat;
            this.stat = value;
            this.duration = duration;
            this.tickrate = tickrate;
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

        [SerializeField] private float tickrate;
        public float TickRate => tickrate; // every Tickrate seconds the effect is applyed
                                           // 1/Tickrate = TicksPerSecond => 1 / 0.2f = 5 => this skill ticks 5 times per second
    }
}
