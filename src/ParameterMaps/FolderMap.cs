using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;

namespace s7.cmDo.ParameterMaps
{
    public class FolderMap : ParameterToFieldMapBase
    {
        public FolderMap() : base("*", task => task.Folder) { }

        public override void Visit(Task item, string value)
        {
            Folder toSet = null;
            foreach (Folder f in ToodleDo.Folders)
            {
                if (f.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase) || f.Name.ToLower().Split(' ').Contains(value.ToLower()))
                {
                    toSet = f;
                    break;
                }
            }
            if (toSet == null)
                toSet = ToodleDo.AddFolder(value, true);
            SetField(item, toSet);
        }
    }
}
