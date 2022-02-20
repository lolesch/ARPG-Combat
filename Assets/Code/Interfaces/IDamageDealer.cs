using System;

namespace ARPG.Combat
{
    public interface IDamageDealer
    {
        void DealDamage(DamageTaker target, float damage);
    }

    public interface EffectApplier
    {
        void ApplyEffect(IEffectReceiver receiver);//, Effect effect);
    }
}