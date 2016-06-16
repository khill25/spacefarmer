using System;
using System.Collections.Generic;

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

		public double Health;
		public double Stamina;

		public List<Skill> Skills;

		public Item HeldItem;

		Inventory _inventory = new Inventory ();
		public Inventory CharacterInventory {
			get { return _inventory; }
		}

		public WorldPosition CurrentWorldPosition;

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
	}
}

