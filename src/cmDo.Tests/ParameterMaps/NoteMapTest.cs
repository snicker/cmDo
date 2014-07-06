using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class NoteMapTest : ParameterMapTestBase<NoteMap>
    {
        [Test]
        public void test_new_context_mapped()
        {
            Map.Visit(TestTask, TestString);
            Assert.AreEqual(TestString, TestTask.Note);
        }
    }
}
