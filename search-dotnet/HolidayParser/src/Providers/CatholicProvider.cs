using HolidayParser.src.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HolidayParser.src.Providers
{
    public class CatholicProvider : ICatholicProvider
    {
        private static readonly ConcurrentDictionary<int, DateTime> _cache = new ConcurrentDictionary<int, DateTime>();

        public DateTime GetEasterSunday(int year)
        {
            return _cache.GetOrAdd(year, y =>
            {
                //http://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year

                var moonMetonicCycle = y % 19; // occurs every 19 years, do % to get which cycle this year fall into
                var century = y / 100;
                var diffEquinoxToFullMoon = (century - century / 4 - (8 * century + 13) / 25 + 19 * moonMetonicCycle + 15) % 30;
                var i = diffEquinoxToFullMoon - (diffEquinoxToFullMoon / 28) * (1 - (diffEquinoxToFullMoon / 28) * (29 / (diffEquinoxToFullMoon + 1)) * ((21 - moonMetonicCycle) / 11));

                var day = i - ((y + (int)(y / 4) + i + 2 - century + (int)(century / 4)) % 7) + 28;
                var month = 3;

                if (day > 31)
                {
                    month++;
                    day -= 31;
                }

                return new DateTime(y, month, day);
            });
        }

        public PublicHoliday GetEasterSunday(string localName, int year, CountryCode countryCode)
        {
            var easterSunday = this.GetEasterSunday(year);
            return new PublicHoliday(easterSunday, localName, countryCode);
        }

        public PublicHoliday GetGoodFriday(string localName, int year, CountryCode countryCode)
        {
            var easterSunday = this.GetEasterSunday(year);
            return new PublicHoliday(easterSunday.AddDays(-2), localName,countryCode);
        }

        public PublicHoliday GetEasterMonday(string localName, int year, CountryCode countryCode)
        {
            var easterSunday = this.GetEasterSunday(year);
            return new PublicHoliday(easterSunday.AddDays(1), localName, countryCode);

        }
    }
}
