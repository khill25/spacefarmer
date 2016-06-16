using System;
namespace Core {
	public class Farming : Skill {

		public override string Name {
			get { return "Farming"; }
		}

		public override string Description {
			get { return "Farming skill is used to determine chance of extra harvest rewards."; }
		}

		public Farming () {
		}
	}
}

