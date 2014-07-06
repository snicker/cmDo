using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace s7.cmDo.ParameterMaps
{
    public class TagMap : ParameterToFieldMapBase
    {
        public TagMap() : base("%", task => task.Tag) { }
    }
}
