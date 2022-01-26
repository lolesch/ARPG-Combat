using System;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Pawn
{
    [Serializable]
    public struct Effect
    {
        [SerializeField]
        private StatScore stat;

        /// <summary>
        /// The duration of the effect. If set to 0 the effect will be applied instantly
        /// </summary>
        [Range(0, 120)]
        [SerializeField]
        private float duration;

        public Effect(StatScore stat, float duration)
        {
            this.stat = stat;
            this.duration = duration;
        }
    }
}
