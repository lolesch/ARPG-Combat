using ARPG.Enums;
using ARPG.Tools;
using UnityEngine;

namespace ARPG.Pawns
{
    [CreateAssetMenu(fileName = "New Pawn Base Values", menuName = "PwanSettings/BaseValues")]

    public class PawnStats : ScriptableObject
    {
        public StatNameToFloat BaseValues = new();

        private void OnValidate()
        {
            var stats = (StatName[])System.Enum.GetValues(typeof(StatName));
            int statCount = System.Enum.GetNames(typeof(StatName)).Length;
            if (BaseValues.Count != statCount)
            {
                StatNameToFloat tmp = BaseValues;
                BaseValues.Clear();

                for (int i = 0; i < statCount; i++)
                {
                    float amount = tmp.TryGetValue(stats[i], out float value) ? value : 0f;
                    BaseValues.Add(stats[i], amount);
                }
            }
        }
    }

    [System.Serializable] public class StatNameToFloat : UnitySerializedDictionary<StatName, float> { }
}
