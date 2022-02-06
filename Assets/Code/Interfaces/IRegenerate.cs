using ARPG.Enums;

namespace ARPG.Combat
{
    public interface IRegenerate { void Regenerate(StatName max, ResourceName resource, StatName regen); }
}