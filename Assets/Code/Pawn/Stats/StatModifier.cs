using ARPG.Enums;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    [Serializable]
    public class StatModifier
    {
        public StatModifier(float amount, StatModifierType type, EffectApplier origin)
        {
            this.amount = amount;
            this.type = type;
            this.origin = origin;
        }

        public float Amount => amount;
        [Tooltip("The modifier's amount")]
        [SerializeField] private float amount;

        public StatModifierType Type => type;
        [Tooltip("The order in wich modifiers are applied")]
        [SerializeField] private StatModifierType type;

        public EffectApplier Origin => origin;
        [Tooltip("The order in wich modifiers are applied")]
        [SerializeField] private EffectApplier origin;

        public void SetOrigin(EffectApplier origin) => this.origin = origin;
    }
}