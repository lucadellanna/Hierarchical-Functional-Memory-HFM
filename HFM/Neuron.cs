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
		// Receptive fields are lists of neurons whose firing influences the firing of this neuron
		public List<Neuron> ProximalReceptiveField => ProximalSynapses.Select(x => x.AxonFrom).ToList();
		public List<Neuron> BasalReceptiveField => BasalSynapses.Select(x => x.AxonFrom).ToList();
		public List<Neuron> ApicalReceptiveField => ApicalSynapses.Select(x => x.AxonFrom).ToList();

		public bool IsFiring { get { return _isFiring; } private set { _isFiring = value; } }
		private bool _isFiring;

		public void SetFiringStatus() { _isFiring = IsExcited && !IsInhibited; } // triggered at each Brain.ProcessNewStimulus

		private bool IsExcited => ProximalSynapses.Count(x => x.IsActive) >=
									   Constants.NUMBER_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE
					   && !IsInhibited;

		private bool IsPreactivated => BasalSynapses.Count(x => x.IsActive) >=
									Constants.NUMBER_OF_BASAL_SYNAPSES_TO_ACTIVATE
						|| ApicalSynapses.Count(x => x.IsActive) >=
									Constants.NUMBER_OF_APICAL_SYNAPSES_TO_ACTIVATE;

		private bool IsInhibitedByItsOwnColumn => Brain.Neurons
													  .Where(neuron => neuron.Coordinates.X == this.Coordinates.X
																	 && neuron.Coordinates.Y == this.Coordinates.Y)
													  .Any(neuron => neuron.IsPreactivated && !this.IsPreactivated);

		private bool IsInhibitedByOtherColumns
		{
			get
			{
				var nearColumnsActivationScores = Brain.Neurons
													   .Where(neuron => neuron.Coordinates.Z == this.Coordinates.Z)
													   .Where(neuron => neuron.Coordinates.GetDistance(this.Coordinates) <= Constants.COLUMNAR_INHIBITION_DISTANCE)
													   .Select(neuron => neuron.ProximalReceptiveField.Count(n => n.IsFiring))
													   .OrderByDescending(score => score);
				var thisScore = ProximalReceptiveField.Count(neuron => neuron.IsFiring);
				return GetPercentile(nearColumnsActivationScores, thisScore) >= Constants.PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE;
			}
		}

		private bool IsInhibited => IsInhibitedByItsOwnColumn || IsInhibitedByOtherColumns;

		private double GetPercentile(IEnumerable<int> list, int x)
		{
			int less = 0;
			int equal = 0;
			foreach (int item in list)
			{
				if (item < x)
					less++;
				else if (item == x)
					equal++;
			}
			return (200 * less + 100 * equal) / (list.Count() * 2);
		}
	}
}