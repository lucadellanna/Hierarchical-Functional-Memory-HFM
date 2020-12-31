using System;
using System.Collections.Generic;

namespace HFM
{
	public class Brain
	{
		public Brain(List<Stimulus> stimuli)
		{
			SDR.Initialize(stimuli);
			Regions.Add(new Region(this, stimuli, 0));
			Regions.Add(new Region(this, stimuli, 1)); // TODO: this region's stimuli is different
			foreach (var region in Regions)
			{
				region.CreateDimensions(stimuli);
				region.CreateNeurons();
			}
			foreach (var region in Regions)
			{
				region.LinkNeurons();
			}
		}

		public void ProcessNextStimulus(Stimulus stimulus)
		{
			LogSDRoutput();
			foreach (var region in Regions)
			{
				foreach (var neuron in region.Neurons)
				{
					neuron.SetFiringStatus();
				}
				LogRegionOutput(region);
			}
		}

		private static void LogRegionOutput(Region region)
		{
			Console.WriteLine($"Output of Level {region.Level} is: ");
			foreach (var neuron in region.Neurons)
			{
				Console.Write(neuron.IsFiring);
			}
		}

		private static void LogSDRoutput()
		{
			Console.WriteLine($"Output of the SDR is: ");
			foreach (var neuron in SDR.Neurons)
			{
				Console.Write(neuron.IsFiring);
			}
		}

		public List<Region> Regions = new List<Region>();
		public SDR SDR = new SDR();
	};
}