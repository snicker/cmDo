using System;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class StatusMapTest : ParameterMapTestBase<StatusMap>
    {
        [Test]
        public void test_each_status_mapped_by_name()
        {
            foreach (Status p in Enum.GetValues(typeof(Status)))
            {
                Map.Visit(TestTask, Enum.GetName(p.GetType(), (int)p));
                Assert.AreEqual(p, TestTask.Status);
            }
        }

        [Test]
        public void test_each_status_mapped_by_number()
        {
            foreach (Status p in Enum.GetValues(typeof(Status)))
            {
                Map.Visit(TestTask, ((int)p).ToString());
                Assert.AreEqual(p, TestTask.Status);
            }
        }
    }
}
