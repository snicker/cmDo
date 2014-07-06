using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using s7.cmDo;

namespace s7.cmDo.Tests
{
    [TestFixture]
    public class DateParserTest
    {
        [Test]
        public void regular_date()
        {
            DateTime a_week_from_now = DateTime.Now + TimeSpan.FromDays(7);
            string a_week_from_now_string = string.Format("{0}/{1}/{2}", a_week_from_now.Month, a_week_from_now.Day, a_week_from_now.Year);
            DateTime testdate;
            Assert.IsTrue(DateParser.TryParse(a_week_from_now_string, out testdate));
            Assert.AreEqual(a_week_from_now.Day, testdate.Day);
        }

        [Test]
        public void regular_time()
        {
            DateTime ten_thirty_pm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 30, 0);
            DateTime testdate;
            Assert.IsTrue(DateParser.TryParse("10:30 pm", out testdate));
            Assert.AreEqual(ten_thirty_pm.Hour, testdate.Hour);
            Assert.AreEqual(ten_thirty_pm.Minute, testdate.Minute);
        }

        [Test]
        public void tomorrow()
        {
            DateTime tomorrow = DateTime.Now + TimeSpan.FromDays(1);
            DateTime testdate;
            Assert.IsTrue(DateParser.TryParse("how about tomorrow", out testdate));
            Assert.AreEqual(tomorrow.Day, testdate.Day);
        }

        [Test]
        public void yesterday()
        {
            DateTime yesterday = DateTime.Now - TimeSpan.FromDays(1);
            DateTime testdate;
            Assert.IsTrue(DateParser.TryParse("yesterday", out testdate));
            Assert.AreEqual(yesterday.Day, testdate.Day);
        }

        [Test]
        public void today()
        {
            DateTime today = DateTime.Now;
            DateTime testdate;
            Assert.IsTrue(DateParser.TryParse("today", out testdate));
            Assert.AreEqual(today.Day, testdate.Day);
        }

        [Test]
        public void test_monday()
        {
            DateTime monday = DateTime.Now.Next(DayOfWeek.Monday);
            DateTime test_monday;
            Assert.IsTrue(DateParser.TryParse("definitely monday", out test_monday));
            Assert.AreEqual(monday.Day, test_monday.Day);
        }

        [Test]
        public void test_next_thursday()
        {
            DateTime thursday = DateTime.Now.Next(DayOfWeek.Thursday).Next(DayOfWeek.Thursday);
            DateTime test_next_thursday;
            Assert.IsTrue(DateParser.TryParse("next thursday", out test_next_thursday));
            Assert.AreEqual(thursday.Day, test_next_thursday.Day);
        }

        [Test]
        public void test_days_later()
        {
            DateTime three_days_later = DateTime.Now + TimeSpan.FromDays(3);
            DateTime test_three_days_later;
            Assert.IsTrue(DateParser.TryParse("3 days", out test_three_days_later));
            Assert.AreEqual(three_days_later.Day, test_three_days_later.Day);
        }

        [Test]
        public void test_days_ago()
        {
            DateTime three_days_ago = DateTime.Now - TimeSpan.FromDays(3);
            DateTime test_three_days_ago;
            Assert.IsTrue(DateParser.TryParse("3 days ago", out test_three_days_ago));
            Assert.AreEqual(three_days_ago.Day, test_three_days_ago.Day);
        }

        [Test]
        public void test_days_and_hours()
        {
            DateTime five_days_and_eighteen_hours = DateTime.Now + TimeSpan.FromDays(5) + TimeSpan.FromHours(18);
            DateTime test_time;
            Assert.IsTrue(DateParser.TryParse("5 days and 18 hours", out test_time));
            Assert.AreEqual(five_days_and_eighteen_hours.Day, test_time.Day);
            Assert.AreEqual(five_days_and_eighteen_hours.Hour, test_time.Hour);
        }

        [Test]
        public void test_a_week()
        {
            DateTime aweekfromnow = DateTime.Now + TimeSpan.FromDays(7);
            DateTime test_time;
            Assert.IsTrue(DateParser.TryParse("a week", out test_time));
            Assert.AreEqual(aweekfromnow.Date, test_time.Date);
        }

        [Test]
        public void test_a_day()
        {
            DateTime a_day_from_now = DateTime.Now + TimeSpan.FromDays(1);
            DateTime test_time;
            Assert.IsTrue(DateParser.TryParse("a day", out test_time));
            Assert.AreEqual(a_day_from_now.Date, test_time.Date);
        }

        [Test]
        public void test_an_hour()
        {
            DateTime an_hour_from_now = DateTime.Now + TimeSpan.FromHours(1);
            DateTime test_time;
            Assert.IsTrue(DateParser.TryParse("an hour", out test_time));
            Assert.AreEqual(an_hour_from_now.Date, test_time.Date);
            Assert.AreEqual(an_hour_from_now.Hour, test_time.Hour);
        }

        [Test]
        public void abraham_lincoln()
        {
            DateTime eighty_seven = DateTime.Now - TimeSpan.FromDays(87 * 365.25);
            DateTime test_time;
            Assert.IsTrue(DateParser.TryParse("4 score and 7 years ago", out test_time));
            Assert.AreEqual(eighty_seven.Date, test_time.Date);
        }
    }
}
