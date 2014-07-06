using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class DueDateMap : DateTimeFieldMap
    {
        public DueDateMap() : base("#", task => task.Due) { }
        public override int CompareTo(IParameterToFieldMap other)
        {
            if (other is DueTimeMap)
                return 1;
            return base.CompareTo(other);
        }
    }
}
