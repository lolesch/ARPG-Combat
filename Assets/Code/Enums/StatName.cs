namespace ARPG.Enums
{
    [System.Serializable]
    /// The identifier of all stats in the game.
    public enum StatName
    {
        HealthMax = 0,
        HealthPerSecond = 1,
        MovementSpeed = 2,
        Damage = 3,
        ManaMax = 4,
        ManaPerSecond = 5,
    }

    [System.Serializable]
    public enum Resource
    {
        HealthCurrent = 0,
        ManaCurrent = 1,
    }
}