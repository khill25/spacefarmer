using System;
using System.Collections.Generic;
namespace Core {
	public class World {

		private static World _instance;
		public static World Instance {
			get {
				if (_instance == null) {
					_instance = new World ();
				}
				return _instance;
			}
		}

		TileMap _currentMap;
		public TileMap CurrentMap {
			get { return this._currentMap; }
		}

		Calendar _calendar;
		public Calendar WorldCalendar {
			get { return _calendar; }
		}

		public World() {
			_instance = this;
			_calendar = new Calendar ();
			_currentMap = new TileMap (0, 0, 128, 128);


			CropSeed corn = CropSeed.CreateCorn ();
			WorldObject cornObject = new WorldObject() { item = corn };
			_currentMap.PlaceItem (new Position (1, 1), cornObject);

			_calendar.AddTime (Calendar.TimeUnit.Day, 3);
		}

		public void Update(double stepSize) {
			long minutes = (long)(1 * stepSize);
			_calendar.Update (minutes);
			_currentMap.UpdateCropSeeds (_calendar);
		}

		Dictionary<String, Character> characters = new Dictionary<String, Character> ();
		public bool MoveCharacter(Character c, WorldPosition currentPosition, WorldPosition toPosition) {
			return false;
		}

		// Uses acing direction
		public bool Use(Character c, WorldPosition currentPosition, Item itemToUse) {
			return false;
		}

		// Uses acing direction
		// Uses the item in hand if avaliable otherwise just try's to interact with whatever the character is next to
		public bool Interact (Character c, WorldPosition currentPosition) {
			return false;
		}

	}
}

