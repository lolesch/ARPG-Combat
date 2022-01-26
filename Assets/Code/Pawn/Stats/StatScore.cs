using ARPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ARPG.Pawn
{
    /// https://www.youtube.com/watch?v=SH25f3cXBVc kudos to Kryzarel
    [Serializable]
    public class StatScore
    {
        //TODO: handle derived attributes - i.e. strength powers your damage

        public event Action<float> maxHasChanged;

        public StatScore(float baseValue = 0)
        {
            this.baseValue = baseValue;

            RecalculateValues();
        }

        [SerializeField] protected float baseValue;

        [SerializeField] protected readonly List<StatModifier> statModifiers = new List<StatModifier>();
        [SerializeField] protected List<StatModifier> equipmentStatModifiers = new List<StatModifier>();

        [SerializeField] protected float maxValue;
        public float MaxValue => maxValue;

        #region StatModifiers

        public void AddModifier(StatModifier mod)
        {
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
            RecalculateValues();
        }

        protected int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Type < b.Type)
                return -1;
            else if (a.Type > b.Type)
                return 1;

            return 0;
        }

        public bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                RecalculateValues();

                return true;
            }

            return false;
        }

        public bool RemoveAllModifiersOfOrigin(object origin)
        {
            bool wasRemoved = false;

            for (int i = statModifiers.Count; i >= 0; i--)
            {
                if (statModifiers[i].Origin == origin)
                {
                    RecalculateValues();
                    wasRemoved = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return wasRemoved;
        }

        #endregion

        #region EquipmentStatModifiers


        public void SetEquipmentModifiers(List<StatModifier> modifiers)
        {
            if (modifiers is null)
            {
                Debug.LogError("modifiers was null");
                return;
            }

            equipmentStatModifiers = modifiers;

            equipmentStatModifiers.Sort(CompareModifierOrder);
            RecalculateValues();
        }

        #endregion

        public virtual void RecalculateValues()
        {
            float sumValue = baseValue;
            float sumPercentAdd = 0;

            if (statModifiers is null)
            {
                Debug.LogError("statModifiers was null");

                return;
            }

            if (equipmentStatModifiers is null)
            {
                Debug.LogError("equipmentStatModifiers was null");

                return;
            }

            List<StatModifier> allMods =
                statModifiers.Union(equipmentStatModifiers).OrderBy(stat => stat.Type).ToList();

            foreach (StatModifier mod in allMods.Where(stat => stat.Type == StatModifierType.Flat))
                sumValue += mod.Value;

            foreach (StatModifier mod in allMods.Where(x => x.Type == StatModifierType.PercentAdd))
                sumPercentAdd += mod.Value;

            sumValue *= 1 + sumPercentAdd;

            foreach (StatModifier mod in allMods.Where(x => x.Type == StatModifierType.PercentMult))
                sumValue *= 1 + mod.Value;

            maxValue = (float)Math.Round(sumValue, 4);

            maxHasChanged?.Invoke(MaxValue);
        }
    }
}