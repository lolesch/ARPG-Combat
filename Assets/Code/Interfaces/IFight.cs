using ARPG.Enums;

namespace ARPG.Combat
{
    public interface IFight : IDealDamage, ITakeDamage, IRegenerate
    {
    }

    public interface IDealDamage
    {
        void DealDamage(ITakeDamage target, float damage);
    }

    public interface ITakeDamage
    {
        void TakeDamage(float damage);
    }

    public interface IRegenerate
    {
        void Regenerate(StatName stat, StatName regen);
    }
}