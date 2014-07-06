using System;
using System.Collections.Generic;
using System.Linq;
using s7.cmDo;
using s7.cmDo.ParameterMaps;
using System.Text;
using NUnit.Framework;

namespace s7.cmDo.Tests
{
    [TestFixture]
    public class MappingProviderTest
    {
        [Test]
        public void test_mappings_found()
        {
            Assert.NotNull(MappingProvider.Mappings.Count);
            Assert.Greater(MappingProvider.Mappings.Count, 0);
        }

        [Test]
        public void test_no_conflicting_mappings()
        {
            Dictionary<string, IParameterToFieldMap> identifiers = new Dictionary<string, IParameterToFieldMap>();
            foreach(IParameterToFieldMap map in MappingProvider.Mappings)
            {
                if (identifiers.ContainsKey(map.Identifier))
                    Assert.Fail("Mapping conflict for {0}: {1} and {2}", map.Identifier, identifiers[map.Identifier], map);
                identifiers.Add(map.Identifier,map);
            }
        }
    }
}
