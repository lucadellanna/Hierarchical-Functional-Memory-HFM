using System;
namespace HFM
{
	public class Coordinates
	{
		public Coordinates(int x, int y, int z = 0)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public readonly int X, Y, Z;

		public int GetDistance(int x, int y, int z)
		{
			var distance = Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2) + Math.Pow(z - Z, 2));
			return (int)Math.Round(distance, 0);
		}

		public int GetDistance(Coordinates coordinates) => GetDistance(coordinates.X, coordinates.Y, coordinates.Z);

		public int GetDistance(RegionNeuron neuron) => GetDistance(neuron.Coordinates);
	}
}
