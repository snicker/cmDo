using System;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class DueTimeMapTest : ParameterMapTestBase<DueTimeMap>
    {
        [Test]
        public void test_time_is_mapped()
        {
            Map.Visit(TestTask, "10:30 pm");
            Assert.AreEqual(22, TestTask.Due.Hour);
            Assert.AreEqual(30, TestTask.Due.Minute);
        }

        [Test]
        public void test_due_date_not_changed_when_time_is_mapped()
        {
            DateTime nineteen_seventy_nine = new DateTime(1979,7,9);
            TestTask.Due = nineteen_seventy_nine;
            Map.Visit(TestTask, "10:30 pm");
            Assert.AreEqual(nineteen_seventy_nine.Date, TestTask.Due.Date);
            Assert.AreEqual(22, TestTask.Due.Hour);
            Assert.AreEqual(30, TestTask.Due.Minute);
        }
    }
}
