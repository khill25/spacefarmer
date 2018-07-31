using System;

namespace HeadlessEngineTester {
	class MainClass {
		public static void Main(string[] args) {

			Core.World world = new Core.World();

			world.Interact(world.player, world.player.CurrentWorldPosition);

			DateTime lastUpdate = DateTime.UtcNow;
			// Game loop
			var timeStep = 1; // controls simulation time step speed
			while (true) {
				TimeSpan diff = DateTime.UtcNow - lastUpdate;
				if (diff.Ticks >= 1000) {
					world.Update(timeStep);
					lastUpdate = DateTime.UtcNow;
				}
			}

		}
	}
}
