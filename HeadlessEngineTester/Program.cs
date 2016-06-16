using System;

namespace HeadlessEngineTester {
	class MainClass {
		public static void Main(string[] args) {

			Core.World world = new Core.World();

			DateTime lastUpdate = DateTime.UtcNow;
			while (true) {
				TimeSpan diff = DateTime.UtcNow - lastUpdate;
				if (diff.Ticks >= 1000) {
					world.Update(1);
					lastUpdate = DateTime.UtcNow;
				}
			}

		}
	}
}
