namespace HFM
{
	public class SDRNeuron : Neuron
	{
		public SDRNeuron(int x, int y, int z = 0)
		{
			Coordinates = new Coordinates(x, y, z);
		}

		public Coordinates Coordinates;

		public bool IsFiring { get { return _isFiring; } private set { _isFiring = value; } }
		private bool _isFiring;

		public void SetFiringStatus(bool value) { _isFiring = value; }
	}
}