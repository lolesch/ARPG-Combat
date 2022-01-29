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
        void SetCurrentHealth();
        void TakeDamage(float damage);
    }

    public interface IRegenerate
    {
        void Regenerate(StatName max, Resource resource, StatName regen);
    }
}