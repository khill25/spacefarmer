using System;
namespace Core {
	public class WateringCan : Tool {

		public double CurrentWaterLevel;

		public override bool Use (TileMap map, Position p) {
			// water the ground if it's tilled
			// if a plant seed is there, water it
			// if near facing water, then refil
			return base.Use (map, p);
		}
	}
}

