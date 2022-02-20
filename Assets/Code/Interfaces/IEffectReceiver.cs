namespace ARPG.Combat
{
    public interface IEffectReceiver
    {
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