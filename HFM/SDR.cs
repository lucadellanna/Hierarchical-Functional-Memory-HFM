﻿using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class SDR
	{
		// TODO: refactor duplicate code with Region.CreateDimensions & Region.UpdateDimensions

		public static List<SensorialDimension> Dimensions = new List<SensorialDimension>();
		public static int Bandwidth;

		public void Initialize(List<Stimulus> stimuli)
		{
			CreateDimensions(stimuli);
			Bandwidth = Dimensions.Sum(dimension => dimension.Bandwidth);
		}

		public static IEnumerable<bool> GetOutput(Stimulus stimulus)
		{
			var output = new bool[Bandwidth];
			foreach (var sensoryInput in stimulus.SensoryInputs)
			{
				var dimension = Dimensions.Single(dim => dim.Name == stimulus.Label);
				var i = Dimensions.IndexOf(dimension);
				for (int j = 0, maxLength = dimension.GetSDROutput(sensoryInput.Intensity).Length; j < maxLength; j++)
				{
					output[i + j] = dimension.GetSDROutput(sensoryInput.Intensity)[j];
				}
			}
			return output;
		}

		private void CreateDimensions(IEnumerable<Stimulus> stimuli)
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