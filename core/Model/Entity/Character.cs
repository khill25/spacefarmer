using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Core {
	public class Character {
		
		public enum Gender {
			Female = 0,
			Male = 1,
			Transgender = 2
		}

		public enum Facing {
			North = 1,
			East = 2,
			West = 4,
			South = 8
		}

		public Facing FacingDirection;

		public String FirstName;
		public String LastName;
		public String Name {
			get { return FirstName + " " + LastName; }
		}

		public String Description;
		public Gender gender = Gender.Female;
		public int Age = 28;
		public virtual bool isPlayer {
			get { return false; }
		}

		public String Occupation;
		public String Birthday;
		public String HairColor;
		public String EyeColor;
		public String SkinTone;
		public String FavoriteFood;

		public double Health;
		public double Stamina;

		public List<Skill> Skills = new List<Skill>();

		public Item HeldItem;

		Inventory _inventory = new Inventory ();
		public Inventory CharacterInventory {
			get { return _inventory; }
		}

		public WorldPosition CurrentWorldPosition;

		public Schedule CharacterSchedule;
		public Dictionary<Character, double> Relationships = new Dictionary<Character, double>();

		public virtual bool Pickup (WorldObject o) {

			// Don't allow the player to pick up seeds
			if (o.item is CropSeed) return false;

			return CharacterInventory.AddItem (o.item);
		}

		public virtual bool Use (Item itemToUse) {
			return false;
		}

		/*
		 * Try to interact with the held item 
		 */
		public virtual bool Interact (Position p) {
			
			return false;
		}

		public static Character CreateCharacter(Dictionary<string, object> json) {
			
			Character c = new Character();

			c.FirstName = json["first_name"] as string;
			c.LastName = json["last_name"] as string;

			string genderString = json["gender"] as string;
			if (genderString.Equals("female")) {
				c.gender = Gender.Female;
			} else if (genderString.Equals("male")) {
				c.gender = Gender.Male;
			} else if (genderString.Equals("transgender")) {
				c.gender = Gender.Transgender;
			}
			 
			c.Age = Convert.ToInt32(json["age"]);
			c.Birthday = json["birthday"] as string;
			c.Occupation = json["occupation"] as string;
			c.HairColor = json["hair_color"] as string;
			c.EyeColor = json["eye_color"] as string;
			c.SkinTone = json["skin_tone"] as string;
			c.FavoriteFood = json["favorite_food"] as string;

			c.CurrentWorldPosition = new WorldPosition(-1, -1); // Init them into nothing

			// Add any other properties here
			return c;
		}
	}
}

