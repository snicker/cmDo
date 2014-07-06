using System;
using System.Collections.Generic;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo.Tests.ParameterMaps
{
    [TestFixture]
    public class SortingTest
    {
        [Test]
        public void test_due_date_is_set_before_due_time()
        {
            List<IParameterToFieldMap> maps = new List<IParameterToFieldMap>();
            IParameterToFieldMap dtmap = new DueTimeMap();
            IParameterToFieldMap ddmap = new DueDateMap();
            maps.Add(dtmap);
            maps.Add(new NoteMap());
            maps.Add(ddmap);

            Assert.Greater(maps.IndexOf(ddmap), maps.IndexOf(dtmap));

            maps.Sort();

            Assert.Greater(maps.IndexOf(dtmap), maps.IndexOf(ddmap));

        }
    }
}
