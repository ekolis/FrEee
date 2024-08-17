namespace FrEee.Modding
{
	public class ModProgress<T> : Utility.Progress<ModReference<T>, T>
			where T : IModObject
	{
		public ModProgress(T item, long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = 0, long extraIncrementalProgressAfterDelay = 0)
			: base(item, value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
		{
		}
	}
}
