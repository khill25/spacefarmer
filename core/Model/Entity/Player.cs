using System;
using System.Dynamic;

namespace Core {
	public class Player : Character {

		public override bool isPlayer {
			get { return true; }
		}

		public Player () {

			// Farming skill
			this.Skills.Add (new Farming ());

		}


	}
}

