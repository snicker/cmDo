using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class StartDateMap : DateTimeFieldMap
    {
        public StartDateMap() : base(">", task => task.Start) { }
    }
}
