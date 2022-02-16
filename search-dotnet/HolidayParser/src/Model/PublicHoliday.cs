using HolidayParser.src.Model.Extensions;
using System;

namespace HolidayParser.src.Model
{
    public class PublicHoliday
    {

        public DateTime Date { get; private set; }


        public string Name { get; private set; }

        /// <summary>
        /// Is this public holiday every year on the same date
        /// </summary>
        public bool Fixed { get; private set; }


        /// <summary>
        /// The launch year of the public holiday
        /// </summary>
        public int? LaunchYear { get; private set; }
        public CountryCode CountryCode { get; private set; }

        /// <summary>
        /// Add Public Holiday
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="englishName"></param>
        /// <param name="launchYear"></param>
        public PublicHoliday(int year, int month, int day,  string englishName, bool fixedHoliday = false, int? launchYear = null)
        {
            this.Date = new DateTime(year, month, day);

            this.Name = englishName;
    
            this.Fixed = fixedHoliday;
 
            this.LaunchYear = launchYear;

        }

        /// <summary>
        /// Add Public Holiday
        /// </summary>
        /// <param name="date"></param>
        /// <param name="englishName"></param>
        /// <param name="launchYear"></param>
        public PublicHoliday(DateTime date, string englishName, CountryCode countryCode, bool fixedHoliday = false, int? launchYear = null)
        {
            this.Date = date;

            this.Name = englishName;

            this.Fixed = fixedHoliday;

            this.LaunchYear = launchYear;

            this.CountryCode = countryCode;

        }

        /// <summary>
        /// Date and Name of the PublicHoliday
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Date:yyyy-MM-dd} {this.Name}";
        }


        internal PublicHoliday SetLaunchYear(int launchYear)
        {
            this.LaunchYear = launchYear;

            return this;
        }

        internal PublicHoliday Shift(Func<DateTime, DateTime> shiftSaturday, Func<DateTime, DateTime> shiftSunday)
        {
            this.Date = this.Date.Shift(shiftSaturday, shiftSunday);

            return this;
        }

        internal PublicHoliday ShiftWeekdays(Func<DateTime, DateTime> monday = null, Func<DateTime, DateTime> tuesday = null, Func<DateTime, DateTime> wednesday = null, Func<DateTime, DateTime> thursday = null, Func<DateTime, DateTime> friday = null)
        {
            this.Date = this.Date.ShiftWeekdays(monday, tuesday, wednesday, thursday, friday);

            return this;
        }
    }
}
