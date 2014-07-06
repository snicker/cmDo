using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class ContextMap : ParameterToFieldMapBase
    {
        public ContextMap() : base("@", task => task.Context) { }
        public override void Visit(Task item, string value)
        {
            Context toSet = null;
            foreach (Context c in ToodleDo.Contexts)
            {
                if (c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase) || c.Name.ToLower().Split(' ').Contains(value.ToLower()))
                {
                    toSet = c;
                    break;
                }
            }
            if (toSet == null)
                toSet = ToodleDo.AddContext(value);
            SetField(item, toSet);
        }
    }
}
