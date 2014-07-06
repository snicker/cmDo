using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using s7.cmDo.Extensions;

namespace s7.cmDo.ParameterMaps
{
    public class PriorityMap : ParameterToFieldMapBase
    {
        public PriorityMap() : base("!", task => task.Priority) { }
        public override void Visit(Task item, string value)
        {
            Priority p = Priority.Low;
            if (value.StartsWith("!"))
            {
                int priority = Math.Max(1,Math.Min(3,value.Select((c, i) => value.Substring(i).TakeWhile(x => x == c && x == '!').Count()).Max()));
                p = (Priority)priority;
                SetField(item, p);
                return;
            }
            if (Priority.TryParse<Priority>(value, out p) || string.IsNullOrEmpty(value))
                SetField(item, p);
        }
    }
}
