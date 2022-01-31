using ARPG.Enums;

namespace ARPG.Combat
{
    public interface IRegenerate { void Regenerate(StatName max, Resource resource, StatName regen); }
}