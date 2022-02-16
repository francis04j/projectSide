﻿using HolidayParser.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayParser.src.Providers
{
    /// <summary>
    /// NoHolidaysProvider
    /// </summary>
    internal class NoHolidaysProvider : IPublicHolidayProvider
    {
        private static readonly Lazy<IPublicHolidayProvider> _instance =
            new Lazy<IPublicHolidayProvider>(() => new NoHolidaysProvider());

        /// <summary>
        /// Gets the singleton instance of <see cref="NoHolidaysProvider"/>.
        /// </summary>
        public static IPublicHolidayProvider Instance
        {
            get { return _instance.Value; }
        }

        private NoHolidaysProvider() { }

        ///<inheritdoc/>
        public IEnumerable<PublicHoliday> Get(int year)
        {
            return Enumerable.Empty<PublicHoliday>();
        }

        ///<inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return Enumerable.Empty<string>();
        }
    }
}
