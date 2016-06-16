using System;
using System.Collections.Generic;

namespace Core {
	public class Inventory {

		public int MaxItemSlots = 12;
		public List<Item> items = new List<Item>();

		public Inventory () {
		}

		public bool AddItem(Item item) {
			if(CanAddItem (item)) {
				items.Add (item);
				return true;
			}
			return false;
		}

		public bool CanAddItem(Item item) {
			if (items.Count+1 < MaxItemSlots) {
				return true;
			} else {
				return false;
			}
		}
	}
}

