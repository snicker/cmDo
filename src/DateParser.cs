using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace s7.cmDo
{
    public static class DateParser
    {
        private static Regex DayMatch = new Regex("(mon|tues|wed|thu|fri|sat|sun)");
        private static Regex FromDays = new Regex(@"(\d+|a) day");
        private static Regex FromHours = new Regex(@"(\d+|a|an) hour");
        private static Regex FromMinutes = new Regex(@"(\d+|a) min");
        private static Regex FromWeeks = new Regex(@"(\d+|a) week");
        private static Regex FromYears = new Regex(@"(\d+|a) year");
        private static Regex FromFortnights = new Regex(@"(\d+|a) fortnight");
        private static Regex FromScores = new Regex(@"(\d+|a) score");
        
        public static bool TryParse(string datestring, out DateTime result)
        {
            result = DateTime.MinValue;
            datestring = datestring.ToLower();

            //Try the easy way first?
            if (DateTime.TryParse(datestring, out result))
                return true;

            //Try some standard english
            if (datestring.Contains("now"))
                result = DateTime.Now;
            if (datestring.Contains("today"))
                result = DateTime.Today;
            if (datestring.Contains("tomorrow"))
                result = DateTime.Today + TimeSpan.FromDays(1);
            if (datestring.Contains("yesterday"))
                result = DateTime.Today - TimeSpan.FromDays(1);

            //Try some days
            if (DayMatch.IsMatch(datestring))
            {
                result = DateTime.Now.Next(getDayOfWeek(DayMatch.Match(datestring).Groups[1].Value));
                if (datestring.Contains("next") && result != DateTime.MinValue)
                    result += TimeSpan.FromDays(7);
            }

            //Try some typical timespans
            TimeSpan ts = TimeSpan.Zero;
            ts += TimespanMatch(FromDays, datestring, x => TimeSpan.FromDays(x));
            ts += TimespanMatch(FromHours, datestring, x => TimeSpan.FromHours(x));
            ts += TimespanMatch(FromMinutes, datestring, x => TimeSpan.FromMinutes(x));
            ts += TimespanMatch(FromWeeks, datestring, x => TimeSpan.FromDays(x * 7));
            ts += TimespanMatch(FromYears, datestring, x => TimeSpan.FromDays(x * 365.25));
            ts += TimespanMatch(FromFortnights, datestring, x => TimeSpan.FromDays(x * 14));
            ts += TimespanMatch(FromScores, datestring, x => TimeSpan.FromDays(x * 365.25 * 20));
            if (ts > TimeSpan.Zero && datestring.Contains("ago"))
                ts = -ts;
            if (ts != TimeSpan.Zero)
                result = DateTime.Now + ts;

            return result != DateTime.MinValue;
        }

        private static TimeSpan TimespanMatch(Regex regex, string toCheck, Func<double, TimeSpan> span)
        {
            if (regex.IsMatch(toCheck))
            {
                double amount = 0;
                string value = regex.Match(toCheck).Groups[1].Value.ToLower().Trim();
                double.TryParse(value, out amount);
                if (value == "a" || value == "an")
                    amount = 1;
                if (amount > 0)
                    return span(amount);
            }
            return TimeSpan.Zero;
        }

        private static DayOfWeek getDayOfWeek(string dayName)
        {
            switch (dayName.Substring(0, Math.Min(3, dayName.Length)))
            {
                case "mon": return DayOfWeek.Monday;
                case "tue": return DayOfWeek.Tuesday;
                case "wed": return DayOfWeek.Wednesday;
                case "thu": return DayOfWeek.Thursday;
                case "fri": return DayOfWeek.Friday;
                case "sat": return DayOfWeek.Saturday;
                case "sun": return DayOfWeek.Sunday;
            }
            return DayOfWeek.Sunday;
        }

        public static DateTime Next(this DateTime dt, DayOfWeek day)
        {
            int offsetDays = day - dt.DayOfWeek;
            if (offsetDays <= 0)
                offsetDays += 7;
            DateTime result = dt.AddDays(offsetDays);
            return result;
        }
    }
}
