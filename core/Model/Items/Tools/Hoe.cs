using System;
namespace Core {
	public class Hoe : Tool {

		public override bool Use (TileMap map, Position p) {
			// make the tile farmable if the tile is able
			return base.Use (map, p);
		}

	}
}

