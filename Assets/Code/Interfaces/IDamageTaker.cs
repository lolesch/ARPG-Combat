using ARPG.Pawns;

namespace ARPG.Combat
{
    public interface IDamageTaker
    {
        void SetCurrentHealth();
        void TakeDamage(float damage);
    }

    public interface IEffectReceiver
    {
        void TakeDamage(float damage);

        void ApplyBuff(StatusEffect statusEffect);

        void ReceiveEffect(StatusEffect effect);
    }
}