using HolidayParser.src;
using HolidayParser.src.Model;
using System;
using Xunit;

namespace HolidayParserTest
{
    public class EnglandHolidayTest
    {
        [Fact]
        public void Should_Check_Date_IsPublicHoliday()
        {
            var testDate = new DateTime(2017, 08, 28);
            var isPublicHoliday = DateSystem.IsPublicHoliday(testDate, CountryCode.GB, "GB-ENG");
            Assert.True(isPublicHoliday);
        }
    }
}
