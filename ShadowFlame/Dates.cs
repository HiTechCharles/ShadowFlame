using System;
using System.Globalization;

namespace ShadowFlame
{
    public static class Dates
    {
        /// <summary>
        /// Returns the given date as a short date string (culture-specific).
        /// If <paramref name="date"/> is null, <see cref="DateTime.Now"/> is used.
        /// </summary>
        /// <param name="date">Optional date to format.</param>
        /// <returns>Short date string for the resolved date.</returns>
        public static string Short(DateTime? date = null)
        {
            DateTime dt = date ?? DateTime.Now;
            return dt.ToShortDateString();
        }

        /// <summary>
        /// Returns the given date as a long date string (culture-specific).
        /// If <paramref name="date"/> is null, <see cref="DateTime.Now"/> is used.
        /// </summary>
        /// <param name="date">Optional date to format.</param>
        /// <returns>Long date string for the resolved date.</returns>
        public static string Long(DateTime? date = null)
        {
            DateTime dt = date ?? DateTime.Now;
            return dt.ToLongDateString();
        }

        /// <summary>
        /// Returns the given date/time as a short time string (culture-specific).
        /// If <paramref name="date"/> is null, <see cref="DateTime.Now"/> is used.
        /// </summary>
        /// <param name="date">Optional date/time to format.</param>
        /// <returns>Short time string for the resolved date/time.</returns>
        public static string Time(DateTime? date = null)
        {
            DateTime dt = date ?? DateTime.Now;
            return dt.ToShortTimeString();
        }

        /// <summary>
        /// Calculates the next occurrence of the specified day of week relative to
        /// the provided date (or now if null). If the specified day is the same as
        /// the reference date's day, the next week's occurrence is returned.
        /// </summary>
        /// <param name="dayOfWeek">The target day of week.</param>
        /// <param name="from">Optional reference date. If null, DateTime.Now is used.</param>
        /// <returns>The long date string of the next occurrence of <paramref name="dayOfWeek"/>.</returns>
        public static string NextDayOfWeek(DayOfWeek dayOfWeek, DateTime? from = null)
        {
            DateTime dt = from ?? DateTime.Now;
            int daysToAdd = ((int)dayOfWeek - (int)dt.DayOfWeek + 7) % 7;
            if (daysToAdd == 0)
            {
                daysToAdd = 7; // If today is the specified day, return the next occurrence
            }
            DateTime nextDate = dt.AddDays(daysToAdd);
            return nextDate.ToLongDateString();
        }
    }
}
