using System;
using System.Collections.Generic;
using System.Linq;

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
			// TODO: write code to process the stimulus

			foreach (var region in Regions)
			{
				foreach (var neuron in region.Neurons)
				{
					neuron.SetFiringStatus();
				}
			}
		}

		public List<Region> Regions = new List<Region>();
		public SDR SDR = new SDR();
	};
}