using System;
using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class Brain
	{
		public void ProcessNextStimulus(Stimulus stimulus)
		{
			// TODO: write code to process the stimulus
			foreach (var neuron in Neurons) neuron.SetFiringStatus();
		}

		private static List<SensorialDimension> GetDimensions(IEnumerable<Stimulus> stimuli)
		{
			var dimensions = new List<SensorialDimension>();
			var sensoryInputs = stimuli.SelectMany(x => x.SensoryInputs)
									   .Where(x => string.IsNullOrEmpty(x.Label));
			CreateOrAdjustDimensions(dimensions, sensoryInputs);
			foreach (var dimension in dimensions)
			{
				if (dimension.MinValue == dimension.MaxValue) { dimension.MinValue = 0; } // Binary dimension
			}
			return dimensions;
		}

		private static void CreateOrAdjustDimensions(List<SensorialDimension> dimensions, IEnumerable<SensoryInput> sensoryInputs)
		{
			foreach (var input in sensoryInputs)
			{
				if (!dimensions.Any(x => x.Name == input.Dimension)) { dimensions.Add(new SensorialDimension(input.Dimension)); }
				var dimension = dimensions.Single(x => x.Name == input.Dimension);
				if (dimension.MinValue > input.Intensity) { dimension.MinValue = input.Intensity; }
				if (dimension.MaxValue < input.Intensity) { dimension.MaxValue = input.Intensity; }
			}
		}

		public static List<Neuron> Neurons = new List<Neuron>();
	};
}