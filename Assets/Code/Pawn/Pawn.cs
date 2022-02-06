using System.Collections.Generic;
using ARPG.Combat;
using ARPG.Enums;
using ARPG.Input;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns
{
    public class Pawn : Interactable, IDamageTaker, IEffectReceiver
    {
        public Dictionary<StatName, StatScore> stats = new Dictionary<StatName, StatScore>();
        public Dictionary<ResourceName, ResourceScore> resources = new Dictionary<ResourceName, ResourceScore>();

        public List<StatusEffect> activeEffects = new();

        void Update()
        {
            foreach (var effect in activeEffects)
            {
                if (!stats.TryGetValue(effect.StatName, out StatScore stat))
                    return;

                // is overTime-Effect?
                if (0 < effect.Duration)
                {
                    //if (effect.DurationTicker.IsTicking)
                    //{
                    //    effect.DurationTicker.Tick(Time.deltaTime);
                    //
                    //    if (effect.TickrateTicker.IsTicking)
                    //        effect.TickrateTicker.Tick(Time.deltaTime);
                    //    else
                    //    {
                    //        effect.TickrateTicker.Restart();
                    //
                    //        StatModifier statModifier = new(effect.TickValue, effect.Modifier.Type, effect.Modifier.Origin);
                    //
                    //        stat.AddModifier(statModifier);
                    //    }
                }
                else
                    stat.AddModifier(effect.Modifier);
            }
        }

        protected virtual void Awake()
        {
            stats.Add(StatName.HealthMax, new StatScore(100));
            SetCurrentHealth();
        }

        public void SetCurrentHealth()
        {
            if (stats.TryGetValue(StatName.HealthMax, out StatScore healthMax))
            {
                resources.Add(ResourceName.HealthCurrent, new ResourceScore(healthMax));
                if (resources.TryGetValue(ResourceName.HealthCurrent, out ResourceScore healthCurrent))
                    healthCurrent.AddToCurrentValue(healthMax.MaxValue);
            }
        }

        public void TakeDamage(float damage)
        {
            if (stats.TryGetValue(StatName.HealthMax, out StatScore healthMax))
            {
                if (resources.TryGetValue(ResourceName.HealthCurrent, out ResourceScore health))
                {
                    health.AddToCurrentValue(-damage);
                    EditorDebug.Log($"{this.name} took {damage} damage and has {health.CurrentValue} of {healthMax.MaxValue} health ({health.CurrentValue * 100 / healthMax.MaxValue} %)");

                    if (health.CurrentValue <= 0)
                        Kill();
                }
            }
            else
                EditorDebug.Log($"target had no health stats");
        }

        protected override void Interact() => throw new System.NotImplementedException();

        protected virtual void Kill() => Destroy(gameObject);

        public void ReceiveEffect(StatusEffect effect)
        {
            //effect.Apply(this as IEffectReceiver);

            activeEffects.Add(effect);
        }

        public void ApplyBuff(StatusEffect statusEffect)
        {
            throw new System.NotImplementedException();
        }
    }
}