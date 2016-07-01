using System;

namespace Core {
	public class WorldPosition {

		public double X,Y;

		public WorldPosition () {
		}

		public WorldPosition(double x, double y) {
			this.X = x;
			this.Y = y;
		}

		public bool Compare(WorldPosition other) {
			return (X.CompareTo(other.X) + Y.CompareTo(other.Y)) == 0;
		}
	}
}

