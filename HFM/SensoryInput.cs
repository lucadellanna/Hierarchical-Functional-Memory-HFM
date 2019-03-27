using System;
namespace HFM
{
	public class SensoryInput
	{
		public SensoryInput(string dimension, int intensity)
		{
			Dimension = dimension;
			Intensity = intensity;
		}

		public SensoryInput(string label)
		{
			Label = label;
		}

		public readonly string Dimension;
		public readonly int Intensity;
		public string Label { get; }
	}
}
