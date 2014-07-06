using System;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class LengthMapTest : ParameterMapTestBase<LengthMap>
    {
        [Test]
        public void test_new_context_mapped()
        {
            Map.Visit(TestTask, "4");
            Assert.AreEqual(4, TestTask.Length);
        }

        [Test]
        public void test_strange_length_properly_mapped()
        {
            TimeSpan test = TimeSpan.FromDays(14);
            Map.Visit(TestTask, "a fortnight");
            Assert.AreEqual(test.Minutes, TestTask.Length);
        }
    }
}
