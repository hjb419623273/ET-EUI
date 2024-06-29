namespace ET
{    
	public struct NumbericChange
	{
		public Unit Unit;
		public int NumericType;
		public long Old;
		public long New;
	}
	public interface INumericWatcher
	{
		void Run(Unit unit, NumbericChange args);
	}
}
