﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class SDR
	{
		public static List<SensorialDimension> Dimensions = new List<SensorialDimension>();
		public static List<SDRNeuron> Neurons = new List<SDRNeuron>();
		public static int Bandwidth;

		public void Initialize(List<Stimulus> stimuli)
		{
			CreateDimensions(stimuli);
			Bandwidth = Dimensions.Sum(dimension => dimension.Bandwidth);
			var x = Math.Ceiling(Math.Sqrt(Bandwidth));
			var y = Math.Ceiling(Bandwidth / x);
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					Neurons.Add(new SDRNeuron(i, j));
				}
			}
		}

		public static void ProcessStimulus(Stimulus stimulus)
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
			foreach (var bit in output)
			{
				foreach (var neuron in Neurons)
				{
					neuron.SetFiringStatus(bit);
				}
			}
		}

		private void CreateDimensions(IEnumerable<Stimulus> stimuli)
		{
			var sensoryInputs = stimuli.SelectMany(x => x.SensoryInputs)
									   // .Where(x => string.IsNullOrEmpty(x.Label))
									   ;
			UpdateDimensions(sensoryInputs);
			foreach (var dimension in Dimensions)
			{
				if (dimension.MinValue == dimension.MaxValue) { dimension.MinValue = 0; } // Binary dimension
			}
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
