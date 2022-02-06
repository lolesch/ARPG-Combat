using UnityEngine;

namespace ARPG.Enums
{
    /// The int is used as the order the modifiers are applied 
    public enum StatModifierType
    {
        [Tooltip("Flat modifiers are applied additively")]
        Flat = 100,

        [Tooltip("PercentAdd modifiers are summed up before applied multiplicatively")]
        PercentAdd = 200,

        [Tooltip("PercentMult modifiers are applied multiplicatively")]
        PercentMult = 300,
    }
}