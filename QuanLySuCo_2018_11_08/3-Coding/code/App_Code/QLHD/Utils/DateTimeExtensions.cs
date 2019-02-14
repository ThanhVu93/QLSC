using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHD
{
    /// <summary>
    /// Các extension method cho DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static DateTime FirstDayOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1);
        }

        public static DateTime LastDayOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 12, 31);
        }

        /// <summary>
        /// Get quarter number: 1,2,3,4
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetQuarter(this DateTime value)
        {
            return (value.Month - 1) / 3 + 1;
        }

        public static DateTime FirstDayOfQuarter(this DateTime value)
        {
            int quarterNumber = (value.Month - 1) / 3 + 1;
            return new DateTime(value.Year, (quarterNumber - 1) * 3 + 1, 1);
        }

        public static DateTime LastDayOfQuarter(this DateTime value)
        {
            return FirstDayOfQuarter(value).AddMonths(3).AddDays(-1);
        }

        public static DateTime FirstDayOfWeek(this DateTime value, DayOfWeek startOfWeek)
        {
            var diff = value.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return value.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime value, DayOfWeek startOfWeek)
        {
            return FirstDayOfWeek(value, startOfWeek).AddDays(6);
        }

    }
}