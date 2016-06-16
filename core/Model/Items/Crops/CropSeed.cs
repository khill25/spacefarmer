using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Core {
	public class CropSeed : Item {

		bool hasShownHarvestMessage = false;

		public long PlantedTime = -1; // total Calendar Minutes
		public long CurrentGrowTime = 0; // in Calendar ticks (minutes)
		public long TimeNeededToMature; // in calendar ticks (minutes)

		/*
		 * Can you harvest this crop more than once?
		 */
		public bool Regrows;

		public int CurrentStage = 0; // index 
		public List<long> StageLengths = new List<long>(); // in game minutes
		public Dictionary<Calendar.Season, double> GrowthPotentialModifier = new Dictionary<Calendar.Season, double>(){
			{Calendar.Season.Spring, 1},
			{Calendar.Season.Summer, 1},
			{Calendar.Season.Fall, 1},
			{Calendar.Season.Winter, 1},
		};

		public List<long> WateredTime = new List<long> ();

		public bool isPlanted {
			get { return PlantedTime >= 0; }
		}

		public bool isHarvestable {
			get {
				return CurrentGrowTime >= TimeNeededToMature;
			}
		}

		public override ItemType BasicItemType {
			get { return ItemType.Seed; }
		}

		#region SimulationItem implementation

		public void Update (Calendar calendar) {

			long worldTime = calendar.GetTotalTimeUnit (Calendar.TimeUnit.Minute);
			long growTime = worldTime - PlantedTime;
			CurrentGrowTime = growTime;

			UpdateCurrentStage ();

			if (CurrentGrowTime >= TimeNeededToMature && !hasShownHarvestMessage) {
				Debug.WriteLine (Name + " ready to harvest. " + calendar.CurrentDateTime);
				hasShownHarvestMessage = true;
			}
		}

		#endregion

		void UpdateCurrentStage() {
			int stage = CurrentStage;
			long timeRequired = TimeRequiredForStage (stage);
			if(CurrentGrowTime > timeRequired) {
				CurrentStage++;
			}

			// Bounds clamp
			if (CurrentStage >= StageLengths.Count) {
				CurrentStage = StageLengths.Count - 1;
			}
		}

		long TimeRequiredForStage(int stage) {
			long totalTime = 0;
			for (int i = 0; i <= stage; i++) {
				totalTime += StageLengths [i];
			}
			return totalTime;
		}

		public override bool Use (TileMap map, Position p) {
			// Plant this item if the position we are trying to place on
			// is hoed dirt

			Tile t = map.TileAt (p.X, p.Y);
			// pretend it's dirt for now...

			// check tile to make sure it's plantable

			// make sure that we actually have item to plant
			if (base.Use (map, p)) {
				// plant the item
				Plant (World.Instance.WorldCalendar, p);
				return true;
			}

			return false;
		}

		public void Plant (Calendar calendar, Position plantedPosition) {
			this.PlantedTime = calendar.GetTotalTimeUnit (Calendar.TimeUnit.Minute);
		}

		public void Water(Calendar calendar) {
			
		}

		public CropItem Harvest (Calendar calendar) {
			return null;
		}


		public static CropSeed CreateCorn() {
			CropSeed corn = new CropSeed ();
			corn.Name = "Corn";
			corn.id = 1;
			corn.ItemValue = 100;
			corn.CurrentStackSize = 1;
			corn.Description = "Corn used for just about everything.";
			corn.TimeNeededToMature = Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 4);
			corn.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));
			corn.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));
			corn.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));
			corn.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));
			corn.Regrows = true;

			return corn;
		}

		public static CropSeed CreateWheat() {
			CropSeed wheat = new CropSeed ();
			wheat.Name = "Wheat";
			wheat.id = 2;
			wheat.ItemValue = 75;
			wheat.CurrentStackSize = 1;
			wheat.Description = "Used to make cereal, bread, and flour.";
			wheat.TimeNeededToMature = Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 3);
			wheat.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));
			wheat.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));
			wheat.StageLengths.Add (Calendar.TimeUnitValueHelper (Calendar.TimeUnit.Day, 1));

			return wheat;
		}
	}
}

