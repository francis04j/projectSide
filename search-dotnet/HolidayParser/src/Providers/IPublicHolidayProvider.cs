using HolidayParser.src.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HolidayParser.src.Providers
{
    public interface IPublicHolidayProvider
    {
        IEnumerable<PublicHoliday> Get(int year);

        /// <summary>
        ///Get the Holiday Sources
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSources();
    }
}
