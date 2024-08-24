using FrEee.Utility;

namespace FrEee.Objects.GameState
{
	public class GameProgress<T> : Progress<GameReference<T>, T>
    where T : IReferrable
    {
        public GameProgress(T item, long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = 0, long extraIncrementalProgressAfterDelay = 0)
            : base(item, value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
        {
        }
    }
}
