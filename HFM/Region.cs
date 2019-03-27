using System;
using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class Region
	{
		public Region(Brain brain, List<Stimulus> environment, int i)
		{
			Brain = brain;
			Environment = environment;
			Level = i;
		}

		public int Level;
		public Brain Brain;
		public List<Neuron> Neurons = new List<Neuron>();
		public List<Stimulus> Environment = new List<Stimulus>();
		public List<SensorialDimension> Dimensions = new List<SensorialDimension>();

		internal void CreateNeurons()
		{
			var numberOfColumns = Dimensions.Sum(dimension => dimension.Bandwidth);
			var x = Math.Ceiling(Math.Sqrt(numberOfColumns));
			var y = Math.Ceiling(numberOfColumns / x);
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					for (int z = 0; z < Constants.NEURONS_PER_MINICOLUMN; z++)
					{
						Neurons.Add(new Neuron(this, i, j, z));
					}
				}
			}
		}

		internal void LinkNeurons()
		{
			foreach (var neuron in Neurons)
			{
				neuron.Link();
			}
		}

		public void CreateDimensions(IEnumerable<Stimulus> stimuli)
		{
			var dimensions = new List<SensorialDimension>();
			var sensoryInputs = stimuli.SelectMany(x => x.SensoryInputs)
									   .Where(x => string.IsNullOrEmpty(x.Label));
			UpdateDimensions(sensoryInputs);
			foreach (var dimension in dimensions)
			{
				if (dimension.MinValue == dimension.MaxValue) { dimension.MinValue = 0; } // Binary dimension
			}
			Dimensions = dimensions;
		}

		private void UpdateDimensions(IEnumerable<SensoryInput> sensoryInputs)
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
