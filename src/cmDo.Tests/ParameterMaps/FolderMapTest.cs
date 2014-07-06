using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class FolderMapTest : ParameterMapTestBase<FolderMap>
    {
        [Test]
        public void test_new_context_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Folder.Name);
        }

        [Test]
        public void test_existing_context_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Folder.Name);
            Folder c = TestTask.Folder;
            TestTask = new Task();
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(c, TestTask.Folder);
        }

        [Test]
        public void test_partial_name_match_map()
        {
            string name = "A partial name match";
            string partial = "match";
            Map.Visit(TestTask, TestString);
            Map.Visit(TestTask, name);
            Assert.AreEqual(name, TestTask.Folder.Name);
            Folder c = TestTask.Folder;
            Map.Visit(TestTask, "another name");

            TestTask = new Task();
            Map.Visit(TestTask, partial);
            Assert.AreEqual(c, TestTask.Folder);
        }
    }
}
