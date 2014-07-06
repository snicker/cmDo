using System;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class DueDateMapTest : ParameterMapTestBase<DueDateMap>
    {
        [Test]
        public void test_new_context_mapped()
        {
            DateTime now = DateTime.Now;
            Map.Visit(TestTask, now.ToShortDateString());
            Assert.AreEqual(now.Date, TestTask.Due.Date);
        }
    }
}
