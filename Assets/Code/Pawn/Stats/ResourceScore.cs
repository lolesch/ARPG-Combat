using System;
using UnityEngine;

namespace ARPG.Pawns
{
    public class ResourceScore
    {
        // TODO: replace StatScore with float field
        public ResourceScore(StatScore stat)
        {
            this.stat = stat;
            RecalculateValues();
        }

        [SerializeField] private StatScore stat;
        public StatScore Stat => stat;

        public event Action<float> currentHasChanged;

        [SerializeField] private float currentValue;
        public float CurrentValue => currentValue;

        [SerializeField] private float currentPercent = 1;

        private void RecalculateValues()
        {
            /// store the currentValue percentage before recalculating maxValue
            if (0 < Stat.MaxValue)
                currentPercent = CurrentValue / Stat.MaxValue;

            Stat.RecalculateValues();

            /// set currentValue to the percentage it was before recalculating maxValue
            SetCurrentValue(Stat.MaxValue * currentPercent);
        }

        public void AddToCurrentValue(float value)
        {
            currentValue = Mathf.Clamp(currentValue + value, 0, Stat.MaxValue);

            currentHasChanged?.Invoke(CurrentValue * 100 / Stat.MaxValue);
        }

        private void SetCurrentValue(float value)
        {
            currentValue = Mathf.Clamp(value, 0, Stat.MaxValue);

            currentHasChanged?.Invoke(CurrentValue / Stat.MaxValue);
        }

        //public void SetScore(StatScore stat)
        //{
        //    this.stat = stat;
        //}
    }
}