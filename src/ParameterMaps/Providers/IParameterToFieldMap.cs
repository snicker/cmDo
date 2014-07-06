using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public interface IParameterToFieldMap : IComparable<IParameterToFieldMap>
    {
        string Identifier { get; set; }
        void Visit(Task item, string value);
    }
}
