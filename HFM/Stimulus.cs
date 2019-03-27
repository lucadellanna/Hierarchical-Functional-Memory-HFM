using System;
namespace HFM
{
	public class Stimulus
	{
		public Stimulus(string label, int frequency, params SensoryInput[] sensoryInputs)
		{
			Label = label;
			Frequency = frequency;
			SensoryInputs = sensoryInputs;
		}

		public string Label { get; }
		public int Frequency { get; }
		public SensoryInput[] SensoryInputs { get; }
	}
}
