using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public abstract class DateTimeFieldMap : ParameterToFieldMapBase
    {
        public DateTimeFieldMap(string identifier, Expression<Func<Task, object>> expression) : base(identifier,expression) { }
        public override void Visit(Task item, string value)
        {
            DateTime result;
            if(DateParser.TryParse(value, out result))
                SetField(item, result);
        }
    }
}
