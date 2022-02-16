using HolidayParser.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolidayParser.src.Providers
{
    public class WeekendProvider : IWeekendProvider
    {
        /// <summary>
        /// WeekendProvider
        /// </summary>
        /// <param name="weekendDays"></param>
        public WeekendProvider(params DayOfWeek[] weekendDays)
        {
            WeekendDays = weekendDays;

            var min = WeekendDays.Min();
            var max = WeekendDays.Max();

            if (max - min > (min + 7) - max)
            {
                FirstWeekendDay = min;
                LastWeekendDay = max;
            }
            else
            {
                FirstWeekendDay = max;
                LastWeekendDay = min;
            }
        }

        /// <summary>
        /// Returns a WeekendProvider for Saturday and Sunday
        /// </summary>
        public static IWeekendProvider Universal { get; } = new WeekendProvider(DayOfWeek.Saturday, DayOfWeek.Sunday);

        ///<inheritdoc/>
        public IEnumerable<DayOfWeek> WeekendDays { get; }

        ///<inheritdoc/>
        public bool IsWeekend(DateTime date) =>
            this.IsWeekend(date.DayOfWeek);

        ///<inheritdoc/>
        public bool IsWeekend(PublicHoliday publicHoliday) =>
            this.IsWeekend(publicHoliday.Date);

        ///<inheritdoc/>
        public bool IsWeekend(DayOfWeek dayOfWeek) =>
            this.WeekendDays.Contains(dayOfWeek);

        ///<inheritdoc/>
        public DayOfWeek FirstWeekendDay { get; }

        ///<inheritdoc/>
        public DayOfWeek LastWeekendDay { get; }

        /// <summary>
        /// Returns a WeekendProvider for only Friday
        /// </summary>
        public static IWeekendProvider FridayOnly { get; } = new WeekendProvider(DayOfWeek.Friday);

        /// <summary>
        /// Returns a WeekendProvider for only Saturday
        /// </summary>
        public static IWeekendProvider SaturdayOnly { get; } = new WeekendProvider(DayOfWeek.Saturday);

        /// <summary>
        /// Returns a WeekendProvider for only Sunday
        /// </summary>
        public static IWeekendProvider SundayOnly { get; } = new WeekendProvider(DayOfWeek.Sunday);

        /// <summary>
        /// Returns a WeekendProvider for Friday and Sunday
        /// </summary>
        public static IWeekendProvider FridaySunday { get; } = new WeekendProvider(DayOfWeek.Friday, DayOfWeek.Sunday);

        /// <summary>
        /// Returns a WeekendProvider for Friday and Saturday
        /// </summary>
        public static IWeekendProvider SemiUniversal { get; } = new WeekendProvider(DayOfWeek.Friday, DayOfWeek.Saturday);

     

    }
}
