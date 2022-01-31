using ARPG.Enums;
using System;
using System.Collections;

namespace ARPG.Pawns
{
    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModifierType Type;
        private object origin;
        public object Origin => origin;

        // where to handle min and max values and random rolls?

        public StatModifier(float value, StatModifierType type, object origin)
        {
            Value = value;
            Type = type;
            this.origin = origin;
        }

        public void SetOrigin(object origin)
        {
            this.origin = origin;
        }
    }
}