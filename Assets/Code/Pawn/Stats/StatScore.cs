using ARPG.Combat;
using ARPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns
{
    /// inpired by https://www.youtube.com/watch?v=SH25f3cXBVc - kudos to Kryzarel
    [Serializable]
    public class StatScore
    {
        //TODO: handle derived attributes - i.e. strength powers your damageStat

        public event Action<float> maxHasChanged;

        public StatScore(float baseValue = 0)
        {
            this.baseValue = baseValue;

            RecalculateValues();
        }

        protected float baseValue;

        [SerializeField] protected readonly List<StatModifier> tempStatModifiers = new List<StatModifier>();
        [SerializeField] protected List<StatModifier> permanentStatModifiers = new List<StatModifier>();

        [SerializeField] protected float maxValue;
        public float MaxValue => maxValue;

        #region StatModifiers
        public void AddModifier(StatModifier mod)
        {
            tempStatModifiers.Add(mod);
            tempStatModifiers.Sort(CompareModifierOrder);
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
            if (tempStatModifiers.Remove(mod))
            {
                RecalculateValues();

                return true;
            }

            return false;
        }

        public bool RemoveAllModifiersOfOrigin(EffectApplier origin)
        {
            bool wasRemoved = false;

            for (int i = tempStatModifiers.Count; i >= 0; i--)
            {
                if (tempStatModifiers[i].Origin == origin)
                {
                    RecalculateValues();
                    wasRemoved = true;
                    tempStatModifiers.RemoveAt(i);
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
                EditorDebug.LogError("modifiers was null");
                return;
            }

            permanentStatModifiers = modifiers;

            permanentStatModifiers.Sort(CompareModifierOrder);
            RecalculateValues();
        }
        #endregion

        public virtual void RecalculateValues()
        {
            float sumValue = baseValue;
            float sumPercentAdd = 0;

            if (tempStatModifiers is null)
            {
                EditorDebug.LogError("statModifiers was null");

                return;
            }

            if (permanentStatModifiers is null)
            {
                EditorDebug.LogError("equipmentStatModifiers was null");

                return;
            }

            List<StatModifier> allMods =
                tempStatModifiers.Union(permanentStatModifiers).OrderBy(stat => stat.Type).ToList();

            foreach (StatModifier mod in allMods.Where(stat => stat.Type == StatModifierType.Flat))
                sumValue += mod.Amount;

            foreach (StatModifier mod in allMods.Where(x => x.Type == StatModifierType.PercentAdd))
                sumPercentAdd += mod.Amount;

            sumValue *= 1 + sumPercentAdd;

            foreach (StatModifier mod in allMods.Where(x => x.Type == StatModifierType.PercentMult))
                sumValue *= 1 + mod.Amount;

            maxValue = (float)Math.Round(sumValue, 4);

            maxHasChanged?.Invoke(MaxValue);
        }
    }
}