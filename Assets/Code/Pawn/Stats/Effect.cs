using ARPG.Enums;
using System;
using UnityEngine;

namespace ARPG.Pawns
{
    [Serializable]
    public struct Effect
    {
        public Effect(StatName stat, float value, StatModifierType modifier = StatModifierType.Flat, float duration = 0)//, float tickrate = 0.2f)
        {
            this.statName = stat;
            this.amount = value;
            this.modifier = modifier;
            this.duration = duration;
            //this.tickrate = tickrate;
        }

        [SerializeField] private StatName statName;
        public StatName StatName => statName;

        [SerializeField] private float amount;
        public float Amount => amount;

        [SerializeField] private StatModifierType modifier;
        public StatModifierType Modifier => modifier;

        /// <summary>
        /// The duration of the effect. If set to 0 the effect will be applied instantly
        /// </summary>
        [Range(0, 120)]
        [SerializeField] private float duration;
        public float Duration => duration;

        //[SerializeField] private float tickrate;
        //public float TickRate => tickrate; // every Tickrate seconds the effect is applyed
        // 1/Tickrate = TicksPerSecond => 1 / 0.2f = 5 => this skill ticks 5 times per second
    }
}
