using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Collections.Generic;

namespace Core {
	public class Tile : IEquatable<Object>{

		public Position position;

		public Tile(Position p) {
			this.position = p;
		}

		public override int GetHashCode () {
			return (position.X.GetHashCode () + position.Y.GetHashCode ());
		}

//		public override int CompareTo(object other) {
//			Tile t = other as Tile;
//			if (t == null)
//				return -1;
//
//			return t.position.X - this.position.X + t.position.Y - this.position.Y;
//		}

		public override bool Equals(Object other) {

			if (other == null)
				return false;

			Tile t = other as Tile;
			if (t == null)
				return false;

			return t.position.X == this.position.X && t.position.Y == this.position.Y;
		}


	}
}

