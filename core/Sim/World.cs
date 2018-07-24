using System;
using System.Collections.Generic;
using System.Diagnostics;

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

		public Player player;

		Dictionary<String, TileMap> unloadedTileMaps = new Dictionary<String, TileMap>();
		Dictionary<String, TileMap> loadedTileMaps = new Dictionary<String, TileMap>();

		TileMap _currentMap;
		public TileMap CurrentMap {
			get { return this._currentMap; }
		}

		Calendar _calendar;
		public Calendar WorldCalendar {
			get { return _calendar; }
		}

		RelationshipEngine _relationshipEngine;

		public World() {
			_instance = this;
			_calendar = new Calendar ();
			_currentMap = new TileMap (0, 0, 128, 128);

			player = new Player();
			player.FirstName = "Liana";
			player.LastName = "Shadowfall";
			player.CurrentWorldPosition = new WorldPosition(0, 0);
			player.FacingDirection = Character.Facing.East;

			_relationshipEngine = new RelationshipEngine();

			_relationshipEngine.LoadCharacters();
			_relationshipEngine.LoadConversations();

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

		// Uses facing direction
		public bool Use(Character c, WorldPosition currentPosition, Item itemToUse) {
			return false;
		}

		// Uses facing direction
		// Uses the item in hand if avaliable otherwise just try's to interact with whatever the character is next to
		public bool Interact (Character c, WorldPosition currentPosition) {

			// Figure out what character c is next to
			// if it's another character then speak to them

			WorldPosition facingTile = GetCurrentlyFacingTilePosition(c, currentPosition);
			Character tileOccupiedBy = _relationshipEngine.FindCharacter(facingTile);
			// if the facing tile has something that can be interacted with
			// then interact with it

			if (tileOccupiedBy != null) {
				Dialog d = _relationshipEngine.StartConversation(tileOccupiedBy.FirstName);
				if (d != null) {
					if (d.phrase.Contains("%player_name%")) {
						d.phrase = d.phrase.Replace("%player_name%", player.FirstName);
					}
					Debug.WriteLine(d.speaker.FirstName + " says: '" + d.phrase + "'");
				}
				return true;
			}

			return false;
		}

		public WorldPosition GetCurrentlyFacingTilePosition(Character c, WorldPosition currentPosition) {

			WorldPosition modifer = new WorldPosition();
			switch (c.FacingDirection) {
				case Character.Facing.North: modifer.Y = 1; break;
				case Character.Facing.East:  modifer.X = 1; break;
				case Character.Facing.South: modifer.Y = -1; break;
				case Character.Facing.West:  modifer.X = -1; break;
			}

			return new WorldPosition(currentPosition.X + modifer.X, currentPosition.Y + modifer.Y);
		}

		public void UpdateTile(Tile t) {
			
		}

		public void UpdateTileAtPosition(WorldPosition p) {
			
		}

		public List<KeyValuePair<WorldPosition, Character.Facing>> CalculateCharacterPath(WorldPosition from, WorldPosition to) {
			return null;
		}

		public Object GetObjectReferenceForType(Type t) {
			return null;
		}
	}
}

