using HolidayParser.src.Model;
using System;


namespace HolidayParser.src.Providers
{
    public interface ICatholicProvider
    {     
            DateTime GetEasterSunday(int year);

            PublicHoliday GetGoodFriday(string localName, int year, CountryCode countryCode);

            PublicHoliday GetEasterSunday(string localName, int year, CountryCode countryCode);

            PublicHoliday GetEasterMonday(string localName, int year, CountryCode countryCode);
    }
}
