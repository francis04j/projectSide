using HolidayParser.src.Model;
using System;
using System.Collections.Generic;
using HolidayParser.src.Model.Extensions;
using System.Linq;

namespace HolidayParser.src.Providers
{
    public class UnitedKingdomProvider : IPublicHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// UnitedKingdomProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public UnitedKingdomProvider(ICatholicProvider catholicProvider)
        {
            this._catholicProvider = catholicProvider;
        }

        ///<inheritdoc/>
        public IEnumerable<PublicHoliday> Get(int year)
        {
            var countryCode = CountryCode.GB;

            var firstMondayInAugust = DateSystem.FindDay(year, Month.August, DayOfWeek.Monday, Occurrence.First);
            var lastMondayInAugust = DateSystem.FindLastDay(year, Month.August, DayOfWeek.Monday);

            var items = new List<PublicHoliday>();

            #region New Year's Day with fallback

            var newYearDay = new DateTime(year, 1, 1);
            if (newYearDay.IsWeekend(countryCode))
            {
                var newYearDayMonday = DateSystem.FindDay(year, Month.January, 1, DayOfWeek.Monday);
               
                items.Add(new PublicHoliday(newYearDayMonday, "New Year's Day", countryCode));
            }
            else
            {
                items.Add(new PublicHoliday(newYearDay, "New Year's Day", countryCode));
            }

            #endregion

            #region New Year's Day 2 with fallback

            var newYearDay2 = new DateTime(year, 1, 2).Shift(saturday => saturday.AddDays(2), sunday => sunday.AddDays(1));
            items.Add(new PublicHoliday(newYearDay2, "New Year's Day", countryCode));

            #endregion

            items.Add(this._catholicProvider.GetGoodFriday("Good Friday", year, countryCode));
            items.Add(this._catholicProvider.GetEasterMonday("Easter Monday", year, countryCode));
            items.Add(new PublicHoliday(firstMondayInAugust, "Summer Bank Holiday", countryCode));
            items.Add(new PublicHoliday(lastMondayInAugust, "Summer Bank Holiday", countryCode));

            var earlyMayBankHoliday = this.GetEarlyMayBankHoliday(year, countryCode);
            if (earlyMayBankHoliday != null)
            {
                items.Add(earlyMayBankHoliday);
            }

            var springBankHoliday = this.GetSpringBankHoliday(year, countryCode);
            if (springBankHoliday != null)
            {
                items.Add(springBankHoliday);
            }

            var queensPlatinumJubilee = this.GetQueensPlatinumJubilee(year, countryCode);
            if (queensPlatinumJubilee != null)
            {
                items.Add(queensPlatinumJubilee);
            }

            #region Christmas Day with fallback

            var christmasDay = new DateTime(year, 12, 25).Shift(saturday => saturday.AddDays(2), sunday => sunday.AddDays(2));
            items.Add(new PublicHoliday(christmasDay, "Christmas Day", countryCode));

            #endregion

            #region St. Stephen's Day with fallback

            var sanktStehpenDay = new DateTime(year, 12, 26).Shift(saturday => saturday.AddDays(2), sunday => sunday.AddDays(2));
            items.Add(new PublicHoliday(sanktStehpenDay, "Boxing Day", countryCode));

            #endregion

            return items.OrderBy(o => o.Date);
        }

        private PublicHoliday GetSpringBankHoliday(int year, CountryCode countryCode)
        {
            var name = "Spring Bank Holiday";

            if (year == 2022)
            {
                //https://www.gov.uk/government/news/extra-bank-holiday-to-mark-the-queens-platinum-jubilee-in-2022
                return new PublicHoliday(year, 6, 2, name);
            }

            var lastMondayInMay = DateSystem.FindLastDay(year, Month.May, DayOfWeek.Monday);
            return new PublicHoliday(lastMondayInMay, name, countryCode, false, 1971);
        }

        private PublicHoliday GetQueensPlatinumJubilee(int year, CountryCode countryCode)
        {
            if (year == 2022)
            {
                return new PublicHoliday(year, 6, 3, "Queen’s Platinum Jubilee");
            }

            return null;
        }

        private PublicHoliday GetEarlyMayBankHoliday(int year, CountryCode countryCode)
        {
            var holidayName = "Early May Bank Holiday";

            if (year == 2020)
            {
                //https://www.bbc.co.uk/news/uk-48565417
                var secondFridayInMay = DateSystem.FindDay(year, Month.May, DayOfWeek.Friday, Occurrence.Second);
                return new PublicHoliday(secondFridayInMay, holidayName, countryCode, false, 1978);
            }

            var firstMondayInMay = DateSystem.FindDay(year, Month.May, DayOfWeek.Monday, Occurrence.First);
            return new PublicHoliday(firstMondayInMay, holidayName, countryCode, false, 1978);
        }



        ///<inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return new string[]
            {
                "https://en.wikipedia.org/wiki/Public_holidays_in_the_United_Kingdom",
                "https://de.wikipedia.org/wiki/Feiertage_im_Vereinigten_K%C3%B6nigreich"
            };
        }

    }
}
