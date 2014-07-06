using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using s7.cmDo.ParameterMaps;
using Toodledo.Model;

namespace s7.cmDo
{
    public static class MappingProvider
    {
        public static readonly List<IParameterToFieldMap> Mappings = new List<IParameterToFieldMap>();
        static MappingProvider()
        {
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(ParameterToFieldMapBase)) && !x.IsAbstract).ToList<Type>())
            {
                IParameterToFieldMap map = Activator.CreateInstance(t) as IParameterToFieldMap;
                if (map != null)
                    Mappings.Add(map);
            }
            Mappings.Sort();
        }
    }
}
