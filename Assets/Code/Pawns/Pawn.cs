using ARPG.Combat;
using ARPG.Enums;
using ARPG.Input;
using ARPG.Tools;
using System.Collections.Generic;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns
{
    public abstract class Pawn : Interactable, IRegenerate, IEffectReceiver
    {
        [SerializeField] private PawnStats pawnStats;

        public StatNameToScore stats = new();

        public ResourceNameToScore resources = new();

        public List<ResourceOverTimeEffect> activeResourceEffects = new();
        public List<StatusEffect> activeStatusEffects = new();
        public List<ConditionEffect> activeConditionEffects = new();

        protected virtual void Awake()
        {
            foreach (var value in pawnStats.BaseValues)
                stats.Add(value.Key, new StatScore(value.Value));

            SetCurrentHealth();
        }

        protected virtual void Update()
        {
            for (int i = activeResourceEffects.Count - 1; 0 <= i; i--)
            {
                var effect = activeResourceEffects[i];

                if (effect.Ticker.HasRemainingDuration)
                {
                    effect.Ticker.Tick(Time.deltaTime);

                    //effect.OverTimeEffect(this, Time.deltaTime);

                    ApplyEffect(effect);
                }
                else
                    effect.RemoveEffect(this);
            }

            void ApplyEffect(ResourceOverTimeEffect effect)
            {
                if (effect.RepeatTicker.HasRemainingDuration)
                    effect.RepeatTicker.Tick(Time.deltaTime);
                else
                {
                    effect.RepeatTicker.Restart();

                    switch (effect.ResourceName)
                    {
                        case ResourceEffectName.Heal:
                            AddToCurrentHealth(effect.Amount / Mathf.RoundToInt(effect.Duration / effect.RepeatTicker.duration));
                            break;
                        case ResourceEffectName.Damage:
                            AddToCurrentHealth(-effect.Amount / Mathf.RoundToInt(effect.Duration / effect.RepeatTicker.duration));
                            break;
                        default:
                            break;
                    }
                }
            }

            Regenerate(ResourceName.HealthCurrent, StatName.HealthPerSecond);
        }

        protected void SetCurrentHealth()
        {
            if (stats.TryGetValue(StatName.HealthMax, out StatScore healthMax))
            {
                resources.Add(ResourceName.HealthCurrent, new ResourceScore(healthMax));
                if (resources.TryGetValue(ResourceName.HealthCurrent, out ResourceScore healthCurrent))
                    healthCurrent.AddToCurrentValue(healthMax.MaxValue);
            }
        }

        public void AddToCurrentHealth(float amount)
        {
            if (resources.TryGetValue(ResourceName.HealthCurrent, out ResourceScore health))
            {
                health.AddToCurrentValue(amount);

                if (health.CurrentValue <= 0)
                    Kill();
            }
            else
                EditorDebug.Log($"target had no health stats");
        }

        public void Regenerate(ResourceName resource, StatName regen)
        {
            if (resources.TryGetValue(resource, out ResourceScore current))
                if (current.CurrentValue < current.Stat.MaxValue)
                    if (stats.TryGetValue(regen, out StatScore regenValue))
                        current.AddToCurrentValue(regenValue.MaxValue * Time.deltaTime);
        }

        protected override void Interact() => throw new System.NotImplementedException();

        protected abstract void Kill();

        public void ReceiveResourceEffect(ResourceEffect effect)
        {
            switch (effect.ResourceName)
            {
                case ResourceEffectName.Heal:
                    AddToCurrentHealth(effect.Amount);
                    break;

                case ResourceEffectName.Damage:
                    AddToCurrentHealth(-effect.Amount);
                    break;
                default:
                    break;
            }
        }
        public void RemoveResourceEffect(ResourceEffect effect) { }

        public void ReceiveResourceOverTimeEffect(ResourceOverTimeEffect effect) => activeResourceEffects.Add(effect);
        public void RemoveResourceOverTimeEffect(ResourceOverTimeEffect effect) => activeResourceEffects.Remove(effect);

        public void ReceiveStatusEffect(StatusEffect effect)
        {
            // add statMod to corresponding Stat
            if (stats.TryGetValue(effect.StatName, out StatScore stat))
                stat.AddModifier(effect.Modifier);

            // TODO: remove statMod after duration
        }
        public void RemoveStatusEffect(StatusEffect effect)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveConditionEffect(ConditionEffect effect) => throw new System.NotImplementedException();
        public void RemoveConditionEffect(ConditionEffect effect)
        {
            throw new System.NotImplementedException();
        }
    }

    [System.Serializable] public class StatNameToScore : UnitySerializedDictionary<StatName, StatScore> { }
    [System.Serializable] public class ResourceNameToScore : UnitySerializedDictionary<ResourceName, ResourceScore> { }
}