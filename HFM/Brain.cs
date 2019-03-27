using System;
using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class Brain
	{
		public Brain(List<Stimulus> environment)
		{
			Environment = environment;
			Regions.Add(new Region(environment, 0));
			foreach (var region in Regions)
			{
				region.CreateDimensions(Environment);
				region.CreateNeurons();
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
		public List<Stimulus> Environment = new List<Stimulus>();
	};
}