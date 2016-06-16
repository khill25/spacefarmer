using System;
using System.Collections.Generic;

namespace Core {
	public class Skill {

		public virtual String Name { get; set;}
		public virtual String Description { get; set; }
		public int CurrentLevel { get; set; }
		public int MaxLevel { get; set; }
		public double TotalXP { get; set; }
		public Dictionary<int, double> XPLevelMap;

		public void ApplyXP(double xpApplied) {
			TotalXP += xpApplied;
		}

	}
}

