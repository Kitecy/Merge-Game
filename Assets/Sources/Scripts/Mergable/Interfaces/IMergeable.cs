namespace MergeGame
{
    public interface IMergeable
    {
        int Level { get; }
        IMergeable Next { get; }

        bool TryMerge(IMergeable mergeable, out IMergeable result);

        bool Compare(IMergeable mergeable);
    }
}
