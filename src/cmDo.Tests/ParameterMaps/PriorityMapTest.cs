using System;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class PriorityMapTest : ParameterMapTestBase<PriorityMap>
    {
        [Test]
        public void test_each_priority_mapped_by_name()
        {
            foreach (Priority p in Enum.GetValues(typeof(Priority)))
            {
                Map.Visit(TestTask,Enum.GetName(p.GetType(), (int)p));
                Assert.AreEqual(p, TestTask.Priority);
            }
        }

        [Test]
        public void test_each_priority_mapped_by_number()
        {
            foreach (Priority p in Enum.GetValues(typeof(Priority)))
            {
                Map.Visit(TestTask, ((int)p).ToString());
                Assert.AreEqual(p, TestTask.Priority);
            }
        }

        [Test]
        public void test_priority_mapped_by_consecutive_exclaimation_points()
        {
            Map.Visit(TestTask, "!");
            Assert.AreEqual((Priority)1, TestTask.Priority);
            Map.Visit(TestTask, "!!");
            Assert.AreEqual((Priority)2, TestTask.Priority);
            Map.Visit(TestTask, "!!!");
            Assert.AreEqual((Priority)3, TestTask.Priority);
        }
    }
}
