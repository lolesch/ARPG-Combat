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
        // => new class DerivedStats with a list of stats and a modifier of the stat conversion
        // foreach stat add that much to it's modifiers

        public event Action<StatScore> maxHasChanged;

        public StatScore(float baseValue = 0)
        {
            this.baseValue = baseValue;

            RecalculateValues();
        }

        public readonly float baseValue;

        [SerializeField] protected readonly List<StatModifier> statModifiers = new List<StatModifier>();

        [SerializeField] protected float maxValue;
        public float MaxValue => maxValue;

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

        public bool RemoveAllModifiersOfOrigin(IEffectApplier origin)
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

        public virtual void RecalculateValues()
        {
            float sumValue = baseValue;
            float sumPercentAdd = 0;

            if (statModifiers is null)
            {
                EditorDebug.LogError("statModifiers was null");

                return;
            }

            statModifiers.OrderBy(stat => stat.Type).ToList();

            foreach (StatModifier mod in statModifiers.Where(stat => stat.Type == StatModifierType.Flat))
                sumValue += mod.Amount;

            foreach (StatModifier mod in statModifiers.Where(x => x.Type == StatModifierType.PercentAdd))
                sumPercentAdd += mod.Amount;

            sumValue *= 1 + sumPercentAdd;

            foreach (StatModifier mod in statModifiers.Where(x => x.Type == StatModifierType.PercentMult))
                sumValue *= 1 + mod.Amount;

            maxValue = (float)Math.Round(sumValue, 4);

            maxHasChanged?.Invoke(this);
        }

        public virtual void RecalculateValuesDeleteMe()
        {
            float sumValue = baseValue;

            if (statModifiers is null)
            {
                EditorDebug.LogError("statModifiers was null");

                return;
            }

            statModifiers.OrderBy(stat => stat.Type).ToList();

            sumValue += statModifiers.Where(stat => stat.Type == StatModifierType.Flat).Sum(s => s.Amount);
            sumValue *= 1 + statModifiers.Where(stat => stat.Type == StatModifierType.PercentAdd).Sum(s => s.Amount);

            foreach (StatModifier mod in statModifiers.Where(x => x.Type == StatModifierType.PercentMult))
                sumValue *= 1 + mod.Amount;

            maxValue = (float)Math.Round(sumValue, 4);

            maxHasChanged?.Invoke(this);
        }
        protected virtual void RecalculateValuesDeleteMeToo()
        {
            if (statModifiers == null)
                return;

            var sumValue = baseValue;

            sumValue += statModifiers.Sum(s => s.Amount);

            maxValue = Mathf.Max(sumValue, 0);

            maxHasChanged?.Invoke(this);
        }
    }
}