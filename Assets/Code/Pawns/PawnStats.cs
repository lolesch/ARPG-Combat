using ARPG.Enums;
using ARPG.Tools;
using UnityEngine;

namespace ARPG.Pawns
{
    [CreateAssetMenu(fileName = "New Pawn Base Values", menuName = "PwanSettings/BaseValues")]

    public class PawnStats : ScriptableObject
    {
        public StatNameToFloat BaseValues = new();
    }

    [System.Serializable] public class StatNameToFloat : UnitySerializedDictionary<StatName, float> { }
}
