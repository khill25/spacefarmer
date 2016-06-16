using System;
using System.Dynamic;

namespace Core {
	public class CropItem : Item {

		public CropSeed RelatedCrop; // the crop that this item is grow from

		public override ItemType BasicItemType {
			get { return ItemType.Produce; }
		}

	}
}

