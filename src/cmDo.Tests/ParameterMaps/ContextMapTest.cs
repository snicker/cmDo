using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class ContextMapTest : ParameterMapTestBase<ContextMap>
    {
        [Test]
        public void test_new_context_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Context.Name);
        }

        [Test]
        public void test_existing_context_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Context.Name);
            Context c = TestTask.Context;
            TestTask = new Task();
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(c, TestTask.Context);
        }

        [Test]
        public void test_partial_name_match_map()
        {
            string name = "A partial name match";
            string partial = "match";
            Map.Visit(TestTask, TestString);
            Map.Visit(TestTask, name);
            Assert.AreEqual(name, TestTask.Context.Name);
            Context c = TestTask.Context;
            Map.Visit(TestTask, "another name");

            TestTask = new Task();
            Map.Visit(TestTask, partial);
            Assert.AreEqual(c, TestTask.Context);
        }
    }
}
