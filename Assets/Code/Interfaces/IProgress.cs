namespace ARPG.Tools
{
    public interface IProgress : IAddable, IRemovable, IReset { }

    public interface IAddable { uint Add(uint amountToAdd); }

    public interface IRemovable { uint Remove(uint amountToRemove); }

    public interface IReset { void Reset(uint startAmount, float threshhold); }
}