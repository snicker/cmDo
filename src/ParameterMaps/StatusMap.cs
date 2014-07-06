using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using s7.cmDo.Extensions;

namespace s7.cmDo.ParameterMaps
{
    public class StatusMap : ParameterToFieldMapBase
    {
        public StatusMap() : base("$", task => task.Status) { }
        public override void Visit(Task item, string value)
        {
            Status s = Status.None;
            value = value.Replace(" ", "");
            if (Status.TryParse<Status>(value, out s))
                SetField(item, s);
        }
    }
}
