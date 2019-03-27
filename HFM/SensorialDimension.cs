using System;
namespace HFM
{
	public class SensorialDimension
	{
		public SensorialDimension(string name, int minValue = 0, int maxValue = 0)
		{
			Name = name;
			MinValue = minValue;
			MaxValue = maxValue;
		}

		public string Name;
		public int MinValue;
		public int MaxValue;
		public int Bandwidth => MaxValue - MinValue + Constants.SDR_OVERLAP;

		public bool[] GetSDROutput(int value)
		{
			if (MinValue == 0 && MaxValue == 1) { return GetBinarySDROutput(value); }
			else { return GetNonbinarySDROutput(value); }
		}

		private bool[] GetBinarySDROutput(int value)
		{
			if (value == 0) { return new bool[Bandwidth]; }
			if (value == 1)
			{
				var array = new bool[Bandwidth];
				for (int i = 0; i < array.Length; i++) { array[i] = true; }
				return array;
			}
			throw new ArgumentOutOfRangeException(nameof(value));
		}
		private bool[] GetNonbinarySDROutput(int value)
		{
			var array = new bool[Bandwidth];
			for (int i = value - MinValue; i < value - MinValue + Constants.SDR_OVERLAP; i++) { array[i] = true; }
			return array;
		}
	}
}