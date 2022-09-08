using System;
using UnityEngine;

namespace ARPG.Pawns
{
    public class ResourceScore
    {
        public ResourceScore(StatScore stat)
        {
            this.stat = stat;
            RecalculateValues();
        }

        //void Awake() => Stat.maxHasChanged += RecalculateValues;

        [SerializeField] private StatScore stat;
        public StatScore Stat => stat;

        public event Action<ResourceScore> currentHasChanged;

        [SerializeField] private float currentValue;
        public float CurrentValue => currentValue;

        [SerializeField] private float currentPercent = 1;

        private void RecalculateValues()
        {
            // TODO: FIX ME => this results in mana not regenerating over 60% (its max value as percent)

            ///// store the currentValue percentage before recalculating maxValue
            //if (0 < Stat.MaxValue)
            //    currentPercent = CurrentValue / Stat.MaxValue;
            //
            //Stat.RecalculateValues();
            //
            ///// set currentValue to the percentage it was before recalculating maxValue
            //SetCurrentValue(Stat.MaxValue / 100 * currentPercent);

            var previousMax = Stat.MaxValue;

            Stat.RecalculateValues();

            var newCurrent = CurrentValue + Mathf.Clamp(Stat.MaxValue - previousMax, 0, Stat.MaxValue);

            SetCurrentValue(newCurrent);
        }

        public void AddToCurrentValue(float value) => SetCurrentValue(currentValue + value);
        public void RefillCurrent() => SetCurrentValue(Stat.MaxValue);

        private void SetCurrentValue(float value)
        {
            currentValue = Mathf.Clamp(value, 0, Stat.MaxValue);

            currentHasChanged?.Invoke(this);
        }
    }
}