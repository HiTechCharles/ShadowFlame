using System;
using System.Globalization;
using Xunit;

namespace ShadowFlame.Tests
{
    public class DatesTests
    {
        [Fact]
        public void Short_ReturnsShortDateString_ForGivenDate()
        {
            var original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
                var dt = new DateTime(2020, 2, 3, 4, 5, 6);
                Assert.Equal(dt.ToShortDateString(), Dates.Short(dt));
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }

        [Fact]
        public void Long_ReturnsLongDateString_ForGivenDate()
        {
            var original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
                var dt = new DateTime(2021, 12, 25);
                Assert.Equal(dt.ToLongDateString(), Dates.Long(dt));
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }

        [Fact]
        public void Time_ReturnsShortTimeString_ForGivenDate()
        {
            var original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
                var dt = new DateTime(2000, 1, 1, 14, 30, 0);
                Assert.Equal(dt.ToShortTimeString(), Dates.Time(dt));
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }

        [Fact]
        public void NextDayOfWeek_ReturnsNextOccurrence_WhenSameDay_ReturnsNextWeek()
        {
            // Choose a known date that's a Monday
            var from = new DateTime(2023, 5, 1); // Monday
            var result = Dates.NextDayOfWeek(DayOfWeek.Monday, from);
            var expected = from.AddDays(7).ToLongDateString();
            Assert.Equal(expected, result);
        }
    }
}
