namespace ARPG.Enums
{
    [System.Serializable]
    /// The identifier of all stats in the game.
    public enum StatName
    {
        NONE = 0,

        HealthMax = 1,
        HealthPerSecond = 2,
        ManaMax = 3,
        ManaPerSecond = 4,

        AttackDamage = 5,
        AttackSpeed = 6,
        MovementSpeed = 7,
    }
}