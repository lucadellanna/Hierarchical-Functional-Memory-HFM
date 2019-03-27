using System;
namespace HFM
{
	public class Synapse
	{
		public Synapse(Neuron axonFrom)
		{
			var random = new Random();
			Permanence = GetRandomPermanence(random);
			AxonFrom = axonFrom;
		}

		private double GetRandomPermanence(Random random)
		{
			return (5 + random.Next(0, 2) + random.Next(0, 2)) / 10;
		}

		public override string ToString()
		{
			return $"Synapse of permanence {Permanence} with {AxonFrom}";
		}

		public readonly Neuron AxonFrom;

		public bool IsActive { get { return AxonFrom.IsFiring && Permanence >= 0.7; } }

		#region Permanence

		public double Permanence { get { return _permanence + _temporaryPermanence; } private set { _permanence = value; } }
		private double _permanence;
		private double _temporaryPermanence;

		public void PermanentlyIncreasePermanence()
		{
			_permanence += 0.1;
			if (Permanence > 1) { _permanence = 1; }
		}

		public void PermanentlyDecreasePermanence()
		{
			_permanence -= 0.1;
			if (Permanence < 0) { _permanence = 0; }
		}

		public void TemporarilyIncreasePermanence()
		{
			if (Permanence < 1) { _temporaryPermanence = 0.1; }
		}

		public void TemporarilyDecreasePermanence()
		{
			if (Permanence > 0) { _temporaryPermanence = -0.1; }
		}

		public void UndoTemporaryChanges()
		{
			_temporaryPermanence = 0;
		}

		public void MakeTemporaryChangesPermanent()
		{
			if (_temporaryPermanence > 0) { PermanentlyIncreasePermanence(); }
			if (_temporaryPermanence < 0) { PermanentlyDecreasePermanence(); }
			_temporaryPermanence = 0;
		}

		#endregion
	}
}
