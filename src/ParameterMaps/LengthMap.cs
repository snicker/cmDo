using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class LengthMap : ParameterToFieldMapBase
    {
        public LengthMap() : base("~", task => task.Length) { }
        public override void Visit(Task item, string value)
        {
            int intresult;
            if (int.TryParse(value, out intresult))
            {
                SetField(item, intresult);
                return;
            }
            DateTime dateresult;
            if (DateParser.TryParse(value, out dateresult))
            {
                TimeSpan timeDiff = dateresult - DateTime.Now;
                SetField(item, Math.Abs(timeDiff.Minutes));
            }
        }
    }
}
