namespace ARPG.Enums
{
    public enum DamageType
    {
        NONE = 0,

        Fire,       // Fire crits create a damage over time (DoT) effect on the target. This is presumably a percentage of the initial damage inflicted.
        Cold,       // Cold damage crits freeze the target, making them unable to move or attack for the duration.
        Lightning,  // Lightning damage crits stun the target, making them unable to act.
        Arcane,     // Arcane damage crits silence the target, making them unable to cast spells.
        Poison,     // 
        Holy,       // 
    }
}