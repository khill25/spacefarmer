using System;
using System.Collections.Generic;

namespace Core {
	public class Schedule {

		Dictionary<Calendar.DaysOfWeek, List<ScheduleEvent>> _schedule;

		public Schedule() {
		}

		public void AddEventForDayOfWeek(Calendar.DaysOfWeek day, ScheduleEvent scheduledEvent) {
		}

	}

	public class ScheduleEvent {

		public String description;
		public WorldPosition location;

		public ScheduleEvent() {
		}
	}
}

