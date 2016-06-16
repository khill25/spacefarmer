using System;

namespace Core {

	/*
	 * Started with seconds and then realized that we really want to increment by minutes
	 * So seconds are basically depricated and try when possible to remove them from
	 * the calculations or math them out to get back to using minutes as the base time currency
	 */

	public class Calendar {

		public enum Season {
			Spring = 0,
			Summer = 1,
			Fall = 2,
			Winter = 3,
			length = 4
		};

		public enum DaysOfWeek {
			Monday = 0,
			Tuesday = 1,
			Wednesday = 2,
			Thursday = 3,
			Friday = 4,
			Saturday = 5,
			Sunday = 6
		}

		public enum TimeUnit {
			Second = 0,
			Minute = 1,
			Hour = 2,
			Day = 3,
			Season = 4,
			Year = 5
		}
				
		public static int TIME_CONVERSION_FACTOR = 1;
		public static int SECONDS_IN_MINUTE = 60;
		public static int MINUTES_IN_HOUR = 60;
		public static int HOURS_IN_DAY = 24;
		public static int DAYS_IN_WEEK = 7;
		public static int WEEKS_IN_SEASON = 4;
		public static int DAYS_IN_SEASON = 28;

		public Season CurrentSeason {
			get {
				return (Season)GetPartTimeUnit (TimeUnit.Season);
			}
		}
		private double _currentTime; // gametime milliseconds, IE game epoch, not real world milliseconds. 
		public double CurrentTime {
			get { return _currentTime; }
		}

		public String CurrentDateTime {
			get {
				long day = GetPartTimeUnit (TimeUnit.Day) + 1;
				long season = GetPartTimeUnit (TimeUnit.Season);
				long year = GetPartTimeUnit (TimeUnit.Year) + 1;
				long hours = GetPartTimeUnit (TimeUnit.Hour);
				long minutes = GetPartTimeUnit (TimeUnit.Minute);
				String post = "th";
				if (day == 1 || day == 21) {
					post = "st";
				} else if (day == 2 || day == 22) {
					post = "nd";
				} else if (day == 3 || day == 23) {
					post = "rd";
				}
				return String.Format (CurrentDisplaySeason + " {0:00}" + post + ", Year {1:0} at: {2:00}:{3:00}",day, year, hours, minutes);
			}
		}

		public String DisplayTime {

			get {
				long hours = GetPartTimeUnit (TimeUnit.Hour);
				long minutes = GetPartTimeUnit (TimeUnit.Minute);
				return String.Format ("{0:00}:{1:00}", hours, minutes);
			}

		}

		public int CurrentYear {
			get { 
				return (int)GetTotalTimeUnit (TimeUnit.Year) + 1;
			}
		}

		public int CurrentDay {
			get { 
				return (int)GetPartTimeUnit (TimeUnit.Day) + 1;
			}
		}

		public String CurrentDisplaySeason {
			get {
				long seasons = GetTotalTimeUnit (TimeUnit.Season);
				Season s = (Season)((int)seasons % (int)Season.length);
				return Enum.GetName (typeof(Season), s);
			}
		}
			
		public Calendar () {
		}

		public void Update(long minutesToAdd) {
			AddTime (TimeUnit.Minute, minutesToAdd);
		}

		public long GetTotalTimeUnit(TimeUnit unit) {

			//long seconds = (long)(CurrentTime / 1000); // seconds are milliseconds
			long minutes = (long)(CurrentTime / TIME_CONVERSION_FACTOR);
			long hours = minutes / 60;
			long days = hours / 24;
			long seasons = days / DAYS_IN_SEASON;
			long years = (int)(seasons / (int)Season.length);

			switch(unit) {
			case TimeUnit.Second:
				return (minutes * 60);
			case TimeUnit.Minute:
				return minutes;
			case TimeUnit.Hour:
				return hours;
			case TimeUnit.Day:
				return days;
			case TimeUnit.Season:
				return seasons;
			case TimeUnit.Year:
				return years;
			default:
				return 0;
			}
		}

		public long GetPartTimeUnit(TimeUnit unit) {

			switch(unit) {
			case TimeUnit.Second:
				return GetTotalTimeUnit (TimeUnit.Second);
			case TimeUnit.Minute:
				return (GetTotalTimeUnit (TimeUnit.Minute) % SECONDS_IN_MINUTE);
			case TimeUnit.Hour:
				return (GetTotalTimeUnit (TimeUnit.Hour) % HOURS_IN_DAY);
			case TimeUnit.Day:
				return (GetTotalTimeUnit (TimeUnit.Day) % DAYS_IN_SEASON);
			case TimeUnit.Season:
				return (GetTotalTimeUnit (TimeUnit.Day) % DAYS_IN_SEASON);
			case TimeUnit.Year:
				return (GetTotalTimeUnit (TimeUnit.Season) % (int)Season.length);
			default:
				return 0;
			}
		}

		public void AddTime(TimeUnit unit, long value) {
			_currentTime += TimeUnitValueHelper (unit, value);
		}

		public void SetTime(TimeUnit unit, long value) {
			_currentTime = TimeUnitValueHelper (unit, value);
		}

		public static long TimeUnitValueHelper(TimeUnit unit, long value) {

			long ret = 0;

			switch(unit) {
			case TimeUnit.Second:
				ret = value;
				break;
			case TimeUnit.Minute:
				ret = value * SECONDS_IN_MINUTE;
				break;
			case TimeUnit.Hour:
				ret = value * SECONDS_IN_MINUTE * MINUTES_IN_HOUR;
				break;
			case TimeUnit.Day:
				ret = value * SECONDS_IN_MINUTE * MINUTES_IN_HOUR * HOURS_IN_DAY;
				break;
			case TimeUnit.Season:
				ret = value * SECONDS_IN_MINUTE * MINUTES_IN_HOUR * HOURS_IN_DAY * DAYS_IN_SEASON;
				break;
			case TimeUnit.Year:
				ret = value * SECONDS_IN_MINUTE * MINUTES_IN_HOUR * HOURS_IN_DAY * DAYS_IN_SEASON * (int)Season.length;
				break;
			default:
				ret = 0;
				break;
			}
			// Becasue we don't really care about seconds here, divide out the seconds in minutes
			return (ret / SECONDS_IN_MINUTE);
		}
	}
}

