using ARPG.Enums;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    [Serializable]
    public struct StatusEffect // Buffs and Debuffs
    {
        /// A temporary modification to a pawn’s stat
        public StatusEffect(StatName stat, StatModifier modifier, float duration = 1f)
        {
            this.statName = stat;
            this.modifier = modifier;
            this.duration = duration;
        }

        // TODO: hande stacking of effects

        public StatName StatName => StatName;
        [Tooltip("The identifier of the stat")]
        [SerializeField] private StatName statName;

        public StatModifier Modifier => modifier;
        [Tooltip("the effect's amount and calculation type")]
        [SerializeField] private StatModifier modifier;

        public float Duration => duration;
        [Tooltip("The duration this effect lasts once applied")]
        [SerializeField] private float duration;
    }


    [Serializable]
    public struct HitEffect
    {
        public HitEffect(StatName stat, StatModifier modifier, float duration = 0, float repeatRate = .2f)
        {
            this.statName = stat;
            this.modifier = modifier;
            this.duration = duration;
        }

        // How to handle resources?

        public StatName StatName => StatName;
        [Tooltip("The identifier of the stat")]
        [SerializeField] private StatName statName;

        public StatModifier Modifier => modifier;
        [Tooltip("the effect's amount and calculation type")]
        [SerializeField] private StatModifier modifier;

        public float Duration => duration;
        [Tooltip("The duration this effect lasts once applied")]
        [SerializeField] private float duration;

    }


    [Serializable]
    public struct DamageEffect
    {
        public DamageEffect(float amount, float duration = 0, float repeatRate = .2f)
        {
            this.amount = amount;
            this.duration = duration;
            this.repeatRate = repeatRate;
        }

        // public DamageType type = DamageType.;

        public float Amount => amount;
        [Tooltip("the effect's total amount applied over the total duration")]
        [SerializeField] private float amount;

        public float Duration => duration;
        [Tooltip("The duration this effect lasts once applied")]
        [SerializeField] private float duration;

        public float RepeatRate => repeatRate;
        [Tooltip("The delay in wich a portion of this effect's amount is applyed")]
        [SerializeField] private float repeatRate;
    }
}
