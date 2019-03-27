using System;
using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class Region
	{
		public Region(List<Stimulus> environment, int i)
		{
			Environment = environment;
			Index = i;
		}

		public int Index;
		public List<Neuron> Neurons = new List<Neuron>();
		public List<SensorialDimension> Dimensions = new List<SensorialDimension>();

		internal void CreateNeurons()
		{
			throw new NotImplementedException();
		}

		public List<Stimulus> Environment = new List<Stimulus>();

		public void CreateDimensions(IEnumerable<Stimulus> stimuli)
		{
			var dimensions = new List<SensorialDimension>();
			var sensoryInputs = stimuli.SelectMany(x => x.SensoryInputs)
									   .Where(x => string.IsNullOrEmpty(x.Label));
			CreateOrAdjustDimensions(sensoryInputs);
			foreach (var dimension in dimensions)
			{
				if (dimension.MinValue == dimension.MaxValue) { dimension.MinValue = 0; } // Binary dimension
			}
			Dimensions = dimensions;
		}

		private void CreateOrAdjustDimensions(IEnumerable<SensoryInput> sensoryInputs)
		{
			foreach (var input in sensoryInputs)
			{
				if (!Dimensions.Any(x => x.Name == input.Dimension)) { Dimensions.Add(new SensorialDimension(input.Dimension)); }
				var dimension = Dimensions.Single(x => x.Name == input.Dimension);
				if (dimension.MinValue > input.Intensity) { dimension.MinValue = input.Intensity; }
				if (dimension.MaxValue < input.Intensity) { dimension.MaxValue = input.Intensity; }
			}
		}
	}
}
