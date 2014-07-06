using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class GoalMapTest : ParameterMapTestBase<GoalMap>
    {
        [Test]
        public void test_new_goal_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Goal.Name);
        }

        [Test]
        public void test_existing_goal_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Goal.Name);
            Goal g = TestTask.Goal;
            TestTask = new Task();
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(g, TestTask.Goal);
        }

        [Test]
        public void test_partial_name_match_map()
        {
            string name = "A partial name match";
            string partial = "match";
            Map.Visit(TestTask, TestString);
            Map.Visit(TestTask, name);
            Assert.AreEqual(name, TestTask.Goal.Name);
            Goal g = TestTask.Goal;
            Map.Visit(TestTask, "another name");

            TestTask = new Task();
            Map.Visit(TestTask, partial);
            Assert.AreEqual(g, TestTask.Goal);
        }
    }
}
