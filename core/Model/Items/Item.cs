using System;
using System.Xml;
using System.ServiceModel.Description;

namespace Core {
	public class Item {

		protected long _id;
		public long id {
			get {return _id;}
			set {_id = value;}
		}

		public String Name;
		public String Description;

		protected Decimal _itemValue;
		public Decimal ItemValue {
			get { return _itemValue; }
			set { _itemValue = value;}
		}

		public enum ItemType {
			None = 0,
			Resource = 100,
			Seed = 200,
			Tool = 300,
			Produce = 400
		}
		public virtual ItemType BasicItemType {
			get;
			set;
		}

		public int MaxStackSize = 1000;
		public int CurrentStackSize = 0;
		public double QualityLevel = 1;
		public String OwnerId;

		public virtual bool Use(TileMap map, Position p) {
			if (CurrentStackSize - 1 < 0) return false;
			CurrentStackSize--; // Use the item
			return true;
		}

	}
}

