namespace ARPG.Combat
{
    public interface IEffectReceiver
    {
        // this might change => rework how effects are defined
        void ReceiveResourceEffect(ResourceEffect effect);
        void RemoveResourceEffect(ResourceEffect effect);
        void ReceiveResourceOverTimeEffect(ResourceOverTimeEffect effect);
        void RemoveResourceOverTimeEffect(ResourceOverTimeEffect effect);
        void ReceiveStatusEffect(StatusEffect effect);
        void RemoveStatusEffect(StatusEffect effect);
        void ReceiveConditionEffect(ConditionEffect effect);
        void RemoveConditionEffect(ConditionEffect effect);
    }
}