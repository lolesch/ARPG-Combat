using ARPG.Enums;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ARPG.Combat
{

    [Serializable, ShowInInspector]
    public struct StatModifier
    {
        public StatModifier(float amount, StatModifierType type, IEffectApplier origin)
        {
            this.amount = amount;
            this.type = type;
            this.origin = origin;
        }

        [Tooltip("The modifier's amount")]
        [ShowInInspector] public readonly float amount;

        [Tooltip("The order in wich modifiers are applied")]
        [ShowInInspector] public readonly StatModifierType type;

        [Tooltip("The order in wich modifiers are applied")]
        [ShowInInspector] public readonly IEffectApplier origin;


        public float Amount => amount;
        public StatModifierType Type => type;
        public IEffectApplier Origin => origin;

        //public void SetOrigin(IEffectApplier origin) => this.origin = origin;
    }
}