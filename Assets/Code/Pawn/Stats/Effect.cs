using ARPG.Enums;
using System;
using UnityEngine;

namespace ARPG.Pawn
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

        [SerializeField] private StatName statName;
        [SerializeField] private StatScore stat;

        /// <summary>
        /// The duration of the effect. If set to 0 the effect will be applied instantly
        /// </summary>
        [Range(0, 120)]
        [SerializeField] private float duration;
    }
}
