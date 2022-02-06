namespace ARPG.Enums
{
    [System.Serializable]
    /// The identifier of all stats in the game.
    public enum StatName
    {
        HealthMax = 0,
        HealthPerSecond = 1,
        ManaMax = 2,
        ManaPerSecond = 3,

        MovementSpeed = 4,
    }

    [System.Serializable]
    public enum ResourceName
    {
        HealthCurrent = 0,
        ManaCurrent = 1,
    }

    [System.Serializable]
    public enum EffectName
    {
        Slow
        //Damage = 3,
    }
}