using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class DueTimeMap : ParameterToFieldMapBase
    {
        public DueTimeMap() : base("=", task => task.Due) { }
        public override void Visit(Task item, string value)
        {
            DateTime result;
            if (DateParser.TryParse(value, out result))
            {
                object alreadySet = GetField(item);
                if (alreadySet != null)
                {
                    DateTime alreadySetDT = (DateTime)alreadySet;
                    result = new DateTime(alreadySetDT.Year, alreadySetDT.Month, alreadySetDT.Day, result.Hour, result.Minute, result.Second);
                }
                SetField(item, result);
            }
        }
    }
}
