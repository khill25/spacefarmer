using System;
using System.Collections.Generic;

namespace Core {
	public class TileMap : TileGroup {

		public String MapName;

		List<CropSeed> plantedCrops = new List<CropSeed> ();
		Dictionary<Position, List<WorldObject>> worldObjects = new Dictionary<Position, List<WorldObject>> ();

		public TileMap(int x, int y, int w, int h) : base(x,y,w,h) {
			
		}

		public void UpdateCropSeeds(Calendar calendar) {
			foreach(CropSeed crop in plantedCrops) {
				crop.Update (calendar);
			}
		}

		public bool PlaceItem (Position p, WorldObject item) {
			bool wasPlaced = item.Place (this, p);

			if (wasPlaced) {
				// add item to entity list
				AddItemHelper(p, item);
			}

			return wasPlaced;
		}

		public bool Interact<T> (Position p, T obj) {

			if (obj is Tool) {
				return InteractWithTool (p, obj as Tool);
			}

			return false;
		}

		public bool InteractWithTool (Position p, Tool tool) {
			return false;
		}

		void AddItemHelper (Position p, WorldObject item) {

			if (item.item is CropSeed) {
				plantedCrops.Add (item.item as CropSeed);
			}

			if (worldObjects.ContainsKey (p)) {
				worldObjects [p].Add (item);
			} else {
				worldObjects.Add (p, new List<WorldObject> () { item });
			}
		}
	}
}

