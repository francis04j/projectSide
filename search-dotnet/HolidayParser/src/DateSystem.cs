using HolidayParser.src.Model;
using HolidayParser.src.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolidayParser.src
{

    public static class DateSystem
    {
        private const bool IsObsolete2022 = false;

        private static readonly ICatholicProvider _catholicProvider = new CatholicProvider();
       // private static readonly IOrthodoxProvider _orthodoxProvider = new OrthodoxProvider();

        private static readonly Dictionary<CountryCode, Lazy<IPublicHolidayProvider>> _publicHolidaysProviders =
            new Dictionary<CountryCode, Lazy<IPublicHolidayProvider>>
            {
               { CountryCode.GB, new Lazy<IPublicHolidayProvider>(() => new UnitedKingdomProvider(_catholicProvider))}
            };


        /// <summary>
        /// Get Provider
        /// </summary>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        [Obsolete("Please use GetPublicHolidayProvider instead", error: true)]
        public static IPublicHolidayProvider GetProvider(CountryCode countryCode)
        {
            return GetPublicHolidayProvider(countryCode);
        }

        /// <summary>
        /// GetPublicHolidayProvider
        /// </summary>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static IPublicHolidayProvider GetPublicHolidayProvider(CountryCode countryCode)
        {
            if (_publicHolidaysProviders.TryGetValue(countryCode, out var provider))
            {
                return provider.Value;
            }

            return NoHolidaysProvider.Instance;
        }

        /// <summary>
        /// GetWeekendProvider
        /// </summary>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static IWeekendProvider GetWeekendProvider(CountryCode countryCode)
        {

            return WeekendProvider.Universal;
        }

        #region Public Holidays for a given year

        /// <summary>
        /// Get Public Holidays of a given year
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHolidays(int year, string countryCode)
        {
            if (!Enum.TryParse(countryCode, true, out CountryCode parsedCountryCode) || !Enum.IsDefined(typeof(CountryCode), parsedCountryCode))
            {
                throw new ArgumentException($"Country code {countryCode} is not valid according to ISO 3166-1 ALPHA-2");
            }

            return GetPublicHolidays(year, parsedCountryCode);
        }


        /// <summary>
        /// Get Public Holidays of a given year
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHolidays(int year, CountryCode countryCode)
        {
            var provider = GetPublicHolidayProvider(countryCode);
            return provider.Get(year);
        }

        /// <summary>
        /// Get Public Holidays of a given year
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        [Obsolete("Use GetPublicHolidays instead", IsObsolete2022)]
        public static IEnumerable<PublicHoliday> GetPublicHoliday(int year, CountryCode countryCode)
        {
            return GetPublicHolidays(year, countryCode);
        }

        /// <summary>
        /// Get Public Holidays of a given year
        /// </summary>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <param name="year">The year</param>
        /// <returns></returns>
        [Obsolete("Use GetPublicHoliday instead, the sorting of the parameters was changed", error: true)]
        public static IEnumerable<PublicHoliday> GetPublicHoliday(CountryCode countryCode, int year)
        {
            var provider = GetPublicHolidayProvider(countryCode);
            return provider.Get(year);
        }

        #endregion

        #region Public Holidays for a date range

        /// <summary>
        /// Get Public Holidays of a given date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHolidays(DateTime startDate, DateTime endDate, string countryCode)
        {
            if (!Enum.TryParse(countryCode, true, out CountryCode parsedCountryCode))
            {
                return null;
            }

            return GetPublicHolidays(startDate, endDate, parsedCountryCode);
        }

        /// <summary>
        /// Get Public Holidays of a given date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        [Obsolete("Use GetPublicHolidays instead", IsObsolete2022)]
        public static IEnumerable<PublicHoliday> GetPublicHoliday(DateTime startDate, DateTime endDate, string countryCode)
        {
            return GetPublicHolidays(startDate, endDate, countryCode);
        }

        /// <summary>
        /// Get Public Holidays of a given date range
        /// </summary>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns></returns>
        [Obsolete("Use GetPublicHoliday instead, the sorting of the parameters was changed", error: true)]
        public static IEnumerable<PublicHoliday> GetPublicHoliday(string countryCode, DateTime startDate, DateTime endDate)
        {
            if (!Enum.TryParse(countryCode, true, out CountryCode parsedCountryCode))
            {
                return null;
            }

            return GetPublicHoliday(parsedCountryCode, startDate, endDate);
        }

        /// <summary>
        /// Get Public Holidays of a given date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHolidays(DateTime startDate, DateTime endDate, CountryCode countryCode)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException($"{nameof(endDate)} is before {nameof(startDate)}", nameof(endDate));
            }

            var currentYear = startDate.Year;
            var endYear = endDate.Year;

            while (currentYear <= endYear)
            {
                var items = GetPublicHolidays(currentYear, countryCode);
                foreach (var item in items)
                {
                    if (item.Date.Date >= startDate.Date && item.Date.Date <= endDate.Date)
                    {
                        yield return item;
                    }
                }
                currentYear++;
            }
        }

        /// <summary>
        /// Get Public Holidays of a given date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        [Obsolete("Use GetPublicHolidays instead", IsObsolete2022)]
        public static IEnumerable<PublicHoliday> GetPublicHoliday(DateTime startDate, DateTime endDate, CountryCode countryCode)
        {
            return GetPublicHolidays(startDate, endDate, countryCode);
        }

        /// <summary>
        /// Get Public Holidays of a given date range
        /// </summary>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns></returns>
        [Obsolete("Use GetPublicHoliday instead, the sorting of the parameters was changed", error: true)]
        public static IEnumerable<PublicHoliday> GetPublicHoliday(CountryCode countryCode, DateTime startDate, DateTime endDate)
        {
            return GetPublicHoliday(startDate, endDate, countryCode);
        }

        /// <summary>
        /// Get Worldwide Public Holidays of a given date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            var items = new List<PublicHoliday>();

            foreach (var publicHolidayProvider in _publicHolidaysProviders.Keys)
            {
                items.AddRange(GetPublicHolidays(startDate, endDate, publicHolidayProvider));
            }

            return items;
        }

        #endregion

        #region Check if a date is a Public Holiday

        private static Func<PublicHoliday, bool> GetPublicHolidayFilter(DateTime date, string countyCode = null)
        {
            return o => o.Date == date.Date
                        && (o.LaunchYear == null || date.Year >= o.LaunchYear);
                        
        }

        /// <summary>
        /// Check is a given date a Public Holiday
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static bool IsPublicHoliday(DateTime date, string countryCode)
        {
            if (!Enum.TryParse(countryCode, true, out CountryCode parsedCountryCode) || !Enum.IsDefined(typeof(CountryCode), parsedCountryCode))
            {
                throw new ArgumentException($"Country code {countryCode} is not valid according to ISO 3166-1 ALPHA-2");
            }

            return IsPublicHoliday(date, parsedCountryCode);
        }

        /// <summary>
        /// Check is a given date a Public Holiday
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static bool IsPublicHoliday(DateTime date, CountryCode countryCode)
        {
            var items = GetPublicHolidays(date.Year, countryCode);
            return items.Any(GetPublicHolidayFilter(date));
        }

        /// <summary>
        /// Check is a given date a Public Holiday
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <param name="publicHolidays">if available the public holidays on this date</param>
        /// <returns></returns>
        public static bool IsPublicHoliday(DateTime date, CountryCode countryCode, out PublicHoliday[] publicHolidays)
        {
            var items = GetPublicHolidays(date.Year, countryCode);
            publicHolidays = items.Where(GetPublicHolidayFilter(date)).ToArray();
            return publicHolidays.Any();
        }

        /// <summary>
        /// Check is a given date a Public Holiday
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <param name="countyCode">Federal state</param>
        /// <returns></returns>
        public static bool IsPublicHoliday(DateTime date, CountryCode countryCode, string countyCode)
        {
            var provider = GetPublicHolidayProvider(countryCode);
    

            var items = GetPublicHolidays(date.Year, countryCode);
            return items.Any(GetPublicHolidayFilter(date, countyCode));
        }

        /// <summary>
        /// Check is a given date a Public Holiday
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <param name="countyCode">Federal state</param>
        /// <returns></returns>
        [Obsolete("Use IsPublicHoliday instead", error: true)]
        public static bool IsOfficialPublicHolidayByCounty(DateTime date, CountryCode countryCode, string countyCode)
        {
            return IsPublicHoliday(date, countryCode, countyCode);
        }

        #endregion

        #region Check a date is a Weekend

        /// <summary>
        /// Check is a give date in the Weekend
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <param name="countryCode">Country Code (ISO 3166-1 ALPHA-2)</param>
        /// <returns></returns>
        public static bool IsWeekend(DateTime date, CountryCode countryCode)
        {
            var provider = GetWeekendProvider(countryCode);
            return provider.IsWeekend(date);
        }

        #endregion

        #region Day Finding

        /// <summary>
        /// Find the latest weekday for example monday in a month
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindLastDay(int year, Month month, DayOfWeek day)
        {
            return FindLastDay(year, (int)month, day);
        }

        /// <summary>
        /// Find the latest weekday for example monday in a month
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindLastDay(int year, int month, DayOfWeek day)
        {
            var resultedDay = FindDay(year, month, day, 5);
            if (resultedDay == DateTime.MinValue)
            {
                resultedDay = FindDay(year, month, day, 4);
            }

            return resultedDay;
        }

        /// <summary>
        /// Find the next weekday for example monday from a specific date
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDay(int year, Month month, int day, DayOfWeek dayOfWeek)
        {
            return FindDay(year, (int)month, day, dayOfWeek);
        }

        /// <summary>
        /// Find the next weekday for example monday from a specific date
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDay(int year, int month, int day, DayOfWeek dayOfWeek)
        {
            return FindDay(new DateTime(year, month, day), dayOfWeek);
        }

        /// <summary>
        /// Find the next weekday for example monday from a specific date
        /// </summary>
        /// <param name="date">The search date</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDay(DateTime date, DayOfWeek dayOfWeek)
        {
            var daysNeeded = (int)dayOfWeek - (int)date.DayOfWeek;

            if ((int)dayOfWeek >= (int)date.DayOfWeek)
            {
                return date.AddDays(daysNeeded);
            }

            return date.AddDays(daysNeeded + 7);
        }

        /// <summary>
        /// Find a day between two dates
        /// </summary>
        /// <param name="yearStart">The start year</param>
        /// <param name="monthStart">The start month</param>
        /// <param name="dayStart">The start day</param>
        /// <param name="yearEnd">The end year</param>
        /// <param name="monthEnd">The end month</param>
        /// <param name="dayEnd">The end day</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDayBetween(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd, DayOfWeek dayOfWeek)
        {
            var startDay = new DateTime(yearStart, monthStart, dayStart);
            var endDay = new DateTime(yearEnd, monthEnd, dayEnd);
            var diff = endDay - startDay;
            var days = diff.Days;
            for (var i = 0; i <= days; i++)
            {
                var specificDayDate = startDay.AddDays(i);
                if (specificDayDate.DayOfWeek == dayOfWeek)
                {
                    return specificDayDate;
                }

            }
            return startDay;
        }

        /// <summary>
        /// Find a day between two dates
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDayBetween(DateTime startDate, DateTime endDate, DayOfWeek dayOfWeek)
        {
            return FindDayBetween(startDate.Year, startDate.Month, startDate.Day, endDate.Year, endDate.Month, endDate.Day, dayOfWeek);
        }

        /// <summary>
        /// Find the next weekday for example monday before a specific date
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDayBefore(int year, Month month, int day, DayOfWeek dayOfWeek)
        {
            return FindDayBefore(year, (int)month, day, dayOfWeek);
        }

        /// <summary>
        /// Find the next weekday for example monday before a specific date
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDayBefore(int year, int month, int day, DayOfWeek dayOfWeek)
        {
            var calculationDay = new DateTime(year, month, day);

            if ((int)dayOfWeek < (int)calculationDay.DayOfWeek)
            {
                var daysSubtract = (int)calculationDay.DayOfWeek - (int)dayOfWeek;
                return calculationDay.AddDays(-daysSubtract);
            }
            else
            {
                var daysSubtract = (int)dayOfWeek - (int)calculationDay.DayOfWeek;
                return calculationDay.AddDays(daysSubtract - 7);
            }
        }

        /// <summary>
        /// Find the next weekday for example monday before a specific date
        /// </summary>
        /// <param name="date">The date where the search starts</param>
        /// <param name="dayOfWeek">The name of the day</param>
        /// <returns></returns>
        public static DateTime FindDayBefore(DateTime date, DayOfWeek dayOfWeek)
        {
            return FindDayBefore(date.Year, date.Month, date.Day, dayOfWeek);
        }

        /// <summary>
        /// Find for example the 3th monday in a month
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <param name="occurrence"></param>
        /// <returns></returns>
        public static DateTime FindDay(int year, int month, DayOfWeek day, int occurrence)
        {
            if (occurrence == 0 || occurrence > 5)
            {
                throw new ArgumentException("Occurance is invalid", nameof(occurrence));
            }

            var firstDayOfMonth = new DateTime(year, month, 1);

            //Substract first day of the month with the required day of the week
            var daysNeeded = (int)day - (int)firstDayOfMonth.DayOfWeek;

            //if it is less than zero we need to get the next week day (add 7 days)
            if (daysNeeded < 0)
            {
                daysNeeded += 7;
            }

            //DayOfWeek is zero index based; multiply by the Occurance to get the day
            var resultedDay = (daysNeeded + 1) + (7 * (occurrence - 1));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return DateTime.MinValue;
            }

            return new DateTime(year, month, resultedDay);
        }

        /// <summary>
        /// Find for example the 3th monday in a month
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <param name="occurrence">The occurrence e.g. First</param>
        /// <returns></returns>
        public static DateTime FindDay(int year, Month month, DayOfWeek day, Occurrence occurrence)
        {
            return FindDay(year, (int)month, day, (int)occurrence);
        }

        #endregion
    }

}
