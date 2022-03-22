using ARPG.Enums;
using ARPG.Tools;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    [Serializable]
    public struct StatusEffect : IEffect // Buffs and Debuffs
    {
        // might need to separate PermanentStatusEffects for gear
        // and TemporaryStatusEffects for buffs

        public StatusEffect(StatName stat, StatModifier modifier, float duration = 1f)
        {
            this.statName = stat;
            this.modifier = modifier;
            this.duration = duration;
            this.ticker = new(duration);
        }

        // TODO: hande stacking of effects
        // bool canStack - and refreshOnReapply

        public StatName StatName => statName;
        [Tooltip("The identifier of the stat")]
        [SerializeField] private StatName statName;

        public StatModifier Modifier => modifier;
        [Tooltip("the effect's amount and calculation type")]
        [SerializeField] private StatModifier modifier;

        public float Duration => duration;
        [Tooltip("The duration this effect lasts once applied")]
        [SerializeField] private float duration;

        public Ticker Ticker => ticker;
        [SerializeField] private Ticker ticker;

        public void ApplyEffect(IEffectReceiver receiver) => receiver.ReceiveStatusEffect(this);

        public void RemoveEffect(IEffectReceiver receiver) => receiver.RemoveStatusEffect(this);
    }


    [Serializable]
    public struct ResourceEffect : IEffect
    {
        public ResourceEffect(ResourceEffectName resourceName, float amount)
        {
            this.resourceName = resourceName;
            this.amount = amount;
        }

        // public DamageType type = DamageType.;

        public ResourceEffectName ResourceName => resourceName;
        [Tooltip("The identifier of the stat")]
        [SerializeField] private ResourceEffectName resourceName;

        public float Amount => amount;
        [Tooltip("the effect's total amount applied over the total duration")]
        [SerializeField] private float amount;

        public void ApplyEffect(IEffectReceiver receiver) => receiver.ReceiveResourceEffect(this);

        public void RemoveEffect(IEffectReceiver receiver) => receiver.RemoveResourceEffect(this);
    }

    [Serializable]
    public struct ResourceOverTimeEffect : IEffect
    {
        public ResourceOverTimeEffect(ResourceEffectName resourceName, float amount, float duration, float repeatRate = .2f)
        {
            this.resourceName = resourceName;
            this.amount = amount;
            this.duration = duration;
            this.repeatRate = repeatRate;
            this.ticker = new(duration);
            this.repeatTicker = new(repeatRate);
        }

        // public DamageType type = DamageType.;

        public ResourceEffectName ResourceName => resourceName;
        [Tooltip("The identifier of the stat")]
        [SerializeField] private ResourceEffectName resourceName;

        public float Amount => amount;
        [Tooltip("the effect's total amount applied over the total duration")]
        [SerializeField] private float amount;

        public Ticker Ticker => ticker;
        [SerializeField] private Ticker ticker;

        public Ticker RepeatTicker => repeatTicker;
        [SerializeField] private Ticker repeatTicker;

        public float Duration => duration;
        [Tooltip("The duration this effect lasts once applied")]
        [SerializeField] private float duration;

        public float RepeatRate => repeatRate;
        [Tooltip("The repeatRate of this effect while applied")]
        [SerializeField] private float repeatRate;

        //public void OverTimeEffect(IEffectReceiver receiver, float delta)
        //{
        //    if (RepeatTicker.HasRemainingDuration)
        //        RepeatTicker.Tick(delta);
        //    else
        //    {
        //        RepeatTicker.Restart();
        //
        //        switch (ResourceName)
        //        {
        //            case ResourceEffectName.Heal:
        //                receiver.AddToCurrentHealth(Amount / Mathf.RoundToInt(Duration / RepeatTicker.duration));
        //                break;
        //            case ResourceEffectName.Damage:
        //                receiver.AddToCurrentHealth(-Amount / Mathf.RoundToInt(Duration / RepeatTicker.duration));
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        public void ApplyEffect(IEffectReceiver receiver)
        {
            this.ticker = new(duration, false);
            this.repeatTicker = new(repeatRate, true);
            receiver.ReceiveResourceOverTimeEffect(this);
        }

        public void RemoveEffect(IEffectReceiver receiver) => receiver.RemoveResourceOverTimeEffect(this);
    }

    [Serializable]
    public struct ConditionEffect : IEffect
    {
        public ConditionEffect(CrowdControl cc, float duration = 1)
        {
            this.cc = cc;
            this.duration = duration;
            this.ticker = new(duration);
        }

        public CrowdControl CC => cc;
        [Tooltip("The type of crowd control")]
        [SerializeField] private CrowdControl cc;

        public float Duration => duration;
        [Tooltip("The duration this effect lasts once applied")]
        [SerializeField] private float duration;

        public Ticker Ticker => ticker;
        [SerializeField] private Ticker ticker;

        public void ApplyEffect(IEffectReceiver receiver) => receiver.ReceiveConditionEffect(this);

        public void RemoveEffect(IEffectReceiver receiver) => receiver.RemoveConditionEffect(this);
    }

    public interface IEffect
    {
        void ApplyEffect(IEffectReceiver receiver);

        void RemoveEffect(IEffectReceiver receiver);
    }

    public interface IEffectOverTime : IEffect
    {
        void TickEffectDuration();
    }
}
