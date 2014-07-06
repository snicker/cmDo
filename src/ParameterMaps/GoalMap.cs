using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class GoalMap : ParameterToFieldMapBase
    {
        public GoalMap() : base("+", task => task.Goal) { }
        public override void Visit(Task item, string value)
        {
            Goal toSet = null;
            foreach (Goal c in ToodleDo.Goals)
            {
                if (c.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase) || c.Name.ToLower().Split(' ').Contains(value.ToLower()))
                {
                    toSet = c;
                    break;
                }
            }
            if (toSet == null)
                toSet = ToodleDo.AddGoal(value,Level.Lifetime,0);
            SetField(item, toSet);
        }
    }
}
