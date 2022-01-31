using ARPG.Tools;
using System;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns
{
    public class ExperienceSystem : Progress
    {
        private float levelFactor = 1.189f;

        private uint level;
        public uint Level { get => level; }

        private uint maxLevel;
        public uint MaxLevel { get => maxLevel; }

        public uint Experience { get => amount; }

        public uint ExperienceNormalized { get => Experience / NextLevel; }

        public uint NextLevel { get => (uint)upperBound; }

        public uint ToNextLevel { get => NextLevel - Experience; }

        public ExperienceSystem(uint level = 1, uint maxLevel = 100, uint amount = 0, float upperBound = 100)
        {
            this.level = level;
            this.maxLevel = maxLevel;
            this.amount = amount;
            this.upperBound = upperBound;
        }

        /// <summary>
        /// Adds experience and returns the resulting level
        /// </summary>
        public uint AddExperience(uint amount)
        {
            uint remaining = Add(amount);
            for (int i = 0; i < maxLevel && 0 < remaining; i++)
            {
                level++;
                Reset(0, upperBound * levelFactor);
                remaining = Add(remaining);
            }
            return level;
        }
    }

    public abstract class Progress : IProgress
    {
        protected uint amount = 0;
        protected float upperBound = 100;

        /// <summary>
        /// Adds an amount and returns the remaining amount that couldnt be added
        /// </summary>
        public uint Add(uint amountToAdd)
        {
            if (0 == amountToAdd)
                return amountToAdd;
            if (upperBound < amount)
            {
                EditorDebug.LogError($"progress \t The amount of {amount} has exceded its upper bound of {upperBound} and couldnt be increased");
                return amountToAdd;
            }

            uint added = Math.Min((uint)upperBound - amount, amountToAdd);
            amount += added;
            HasChanged();

            return amountToAdd - added;
        }

        /// <summary>
        /// Removes an amount and returns the remaining amount that couldnt be removed
        /// </summary>
        public uint Remove(uint amountToRemove)
        {
            uint removed = Math.Min(amount, amountToRemove);
            amount -= removed;
            HasChanged();

            return amountToRemove - removed;
        }

        public void Reset(uint startAmount, float threshhold)
        {
            amount = startAmount;
            upperBound = threshhold;
            HasChanged();
        }

        protected virtual void HasChanged() { }
    }
}