using ARPG.Enums;

namespace ARPG.Combat
{
    public interface IRegenerate { void Regenerate(ResourceName resource, StatName regen); }
}