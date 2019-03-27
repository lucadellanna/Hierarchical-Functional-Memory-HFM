using System;
namespace HFM
{
	using System.Collections.Generic;
	using System.Linq;

	public class Neuron
	{
		public Neuron(int x, int y, int z)
		{
			Coordinates = new Coordinates(x, y, z);
		}

		public Coordinates Coordinates;
		public List<Synapse> ProximalSynapses = new List<Synapse>();
		public List<Synapse> BasalSynapses = new List<Synapse>();
		public List<Synapse> ApicalSynapses = new List<Synapse>();
		public List<Neuron> ProximalReceptiveField => ProximalSynapses.Select(x => x.AxonFrom).ToList();
		public List<Neuron> BasalReceptiveField => BasalSynapses.Select(x => x.AxonFrom).ToList();
		public List<Neuron> ApicalReceptiveField => ApicalSynapses.Select(x => x.AxonFrom).ToList();

		public bool IsActive
		{
			get
			{
				return ProximalSynapses.Count(x => x.IsActive) >=
									   Constants.NUMBER_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE;
			}
		}

		public bool IsPreactivated
		{
			get
			{
				return BasalSynapses.Count(x => x.IsActive) >=
									Constants.NUMBER_OF_BASAL_SYNAPSES_TO_ACTIVATE
									|| ApicalSynapses.Count(x => x.IsActive) >=
									Constants.NUMBER_OF_APICAL_SYNAPSES_TO_ACTIVATE;
			}
		}
	}
}
}
