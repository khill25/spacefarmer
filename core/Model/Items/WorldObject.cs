using System;
namespace Core {

	/*
	 * Instance of an object in the world
	 */
	public class WorldObject {

		public Item item;
		public WorldPosition WorldItemPosition;
		public Position TileItemPosition;

		public virtual bool Place (TileMap map, Position p) {
			return item.Use (map, p);
		}

		public virtual bool TryPlace (TileMap map, Position p) {
			throw new NotImplementedException ("TryPlace is not implemented");
		}



	}
}

