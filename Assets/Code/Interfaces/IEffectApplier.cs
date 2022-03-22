namespace ARPG.Combat
{
    public interface IEffectApplier
    {
        void ApplyEffect(IEffectReceiver receiver);//, Effect effect);
    }
}