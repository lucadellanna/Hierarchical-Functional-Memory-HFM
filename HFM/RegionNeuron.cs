using System;
using System.Collections.Generic;
using System.Linq;

namespace HFM
{
	public class RegionNeuron
	{
		public RegionNeuron(Region region, int x, int y, int z)
		{
			Region = region;
			Coordinates = new Coordinates(x, y, z);
		}

		public Coordinates Coordinates;
		public Region Region;
		public List<Synapse> ProximalSynapses = new List<Synapse>();
		public List<Synapse> BasalSynapses = new List<Synapse>();
		public List<Synapse> ApicalSynapses = new List<Synapse>();

		// Receptive fields are lists of neurons whose firing influences the firing of this neuron
		public List<RegionNeuron> ProximalReceptiveField => ProximalSynapses.Select(x => x.AxonFrom).ToList();
		public List<RegionNeuron> BasalReceptiveField => BasalSynapses.Select(x => x.AxonFrom).ToList();
		public List<RegionNeuron> ApicalReceptiveField => ApicalSynapses.Select(x => x.AxonFrom).ToList();

		public int FeedForwardScore => ProximalReceptiveField.Count(neuron => neuron.IsFiring);

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

		private bool IsInhibitedByItsOwnColumn => Region.Neurons
													  .Where(neuron => neuron.Coordinates.X == this.Coordinates.X
																	 && neuron.Coordinates.Y == this.Coordinates.Y)
													  .Any(neuron => neuron.IsPreactivated && !this.IsPreactivated);


		private bool IsInhibitedByOtherColumns
		{
			get
			{
				var nearColumnsFeedForwardScores = Region.Neurons
													   .Where(neuron => neuron.Coordinates.Z == this.Coordinates.Z) // Selecting neurons with same z works because neurons in a column share the same proximal receptive field
													   .Where(neuron => neuron.Coordinates.GetDistance(this) <= Constants.COLUMNAR_INHIBITION_DISTANCE)
													   .Select(neuron => neuron.ProximalReceptiveField.Count(n => n.IsFiring))
													   .OrderByDescending(score => score);
				return GetPercentile(nearColumnsFeedForwardScores, this.FeedForwardScore) >= Constants.PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE;
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

		internal void Link()
		{
			LinkProximalSynapses();
			LinkBasalSynapses();
			LinkApicalSynapses();
		}

		private void LinkApicalSynapses()
		{
			var random = new Random();
			var pool = new List<RegionNeuron>();
			var eligible = Region.Brain.Regions
								 .Where(region => region.Level >= Region.Level)
								 .Where(region => !region.Equals(this))
								 .SelectMany(region => region.Neurons);
			var maxdistance = eligible.Max(neuron => neuron.Coordinates.GetDistance(this));
			foreach (var neuron in eligible)
			{
				var distance = neuron.Coordinates.GetDistance(this);
				if (distance == 0) continue;
				for (int i = 0; i < Math.Pow(maxdistance / distance, 2); i++) // neurons tend to link to nearby peers
				{
					pool.Add(neuron);
				}
			}
			for (int i = 0; i < Constants.NUMBER_OF_BASAL_SYNAPSES; i++)
			{
				var target = pool[random.Next(pool.Count)];
				pool.RemoveAll(neuron => neuron == target);
				BasalSynapses.Add(new Synapse(target));
			}
		}

		private void LinkBasalSynapses()
		{
			var random = new Random();
			var pool = new List<RegionNeuron>();
			var maxdistance = Region.Neurons.Max(neuron => neuron.Coordinates.GetDistance(this));
			foreach (var neuron in Region.Neurons)
			{
				var distance = neuron.Coordinates.GetDistance(this);
				if (distance == 0) continue;
				for (int i = 0; i < Math.Pow(maxdistance / distance, 2); i++) // neurons tend to link to nearby peers
				{
					pool.Add(neuron);
				}
			}
			for (int i = 0; i < Constants.NUMBER_OF_BASAL_SYNAPSES; i++)
			{
				var target = pool[random.Next(pool.Count)];
				pool.RemoveAll(neuron => neuron == target);
				BasalSynapses.Add(new Synapse(target));
			}
		}

		private void LinkProximalSynapses()
		{
			var random = new Random();
			var pool = new List<RegionNeuron>();
			var eligible = Region.Brain.Regions
									 .Where(region => region.Level < Region.Level) // TODO: only select some regions, those nearby
									 .SelectMany(region => region.Neurons);
			var maxdistance = eligible.Max(neuron => neuron.Coordinates.GetDistance(this));
			foreach (var neuron in eligible)
			{
				var distance = neuron.Coordinates.GetDistance(this);
				if (distance == 0) continue;
				for (int i = 0; i < Math.Pow(maxdistance / distance, 2); i++) // neurons tend to link to nearby peers
				{
					pool.Add(neuron);
				}
			}
			for (int i = 0; i < Constants.NUMBER_OF_BASAL_SYNAPSES; i++)
			{
				var target = pool[random.Next(pool.Count)];
				pool.RemoveAll(neuron => neuron == target);
				BasalSynapses.Add(new Synapse(target));
			}
		}
	}
}