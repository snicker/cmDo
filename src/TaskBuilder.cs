using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using s7.cmDo.ParameterMaps;
using s7.cmDo.Cfg;

namespace s7.cmDo
{
    public class TaskBuilder
    {
        private string[] m_TaskInfo;
        private Task m_Task;

        public TaskBuilder(string taskInfo) : this(taskInfo, null) { }
        public TaskBuilder(string taskInfo, Task basetask) : this(taskInfo.Split(' ', '\t'), basetask) { }
        public TaskBuilder(string[] taskInfo) : this(taskInfo, null) { }
        public TaskBuilder(string[] taskInfo, Task basetask)
        {
            m_TaskInfo = taskInfo;
            m_Task = basetask == null ? new Task() : basetask;
        }


        public Task ToTask()
        {
            Task t = m_Task;
            t = ProcessMapsAndName(t);
            t = DetermineDueDate(t);
            t = DetermineStatus(t);
            return t;
        }

        private Task DetermineStatus(Task t)
        {
            if (t.Name.ToLower().Contains("wf") || t.Name.ToLower().Contains("waiting for") || t.Name.ToLower().Contains("waiting on"))
                t.Status = Status.Waiting;
            return t;
        }

        private Task DetermineDueDate(Task t)
        {
            DateTime tryDue;
            if(DateParser.TryParse(t.Name,out tryDue))
                t.Due = tryDue;
            return t;
        }

        private Task ProcessMapsAndName(Task t)
        {
            StringBuilder currentProperty = new StringBuilder();
            StringBuilder name = new StringBuilder();
            IParameterToFieldMap currentMap = null;
            for (int i = 0; i < m_TaskInfo.Length; i++)
            {
                for (int j = 0; j < MappingProvider.Mappings.Count; j++)
                {
                    if (m_TaskInfo[i].StartsWith(MappingProvider.Mappings[j].Identifier))
                    {
                        if (currentMap != MappingProvider.Mappings[j])
                        {
                            if (currentMap != null)
                                currentMap.Visit(t, currentProperty.ToString().Trim());
                            currentProperty = new StringBuilder();
                            currentMap = MappingProvider.Mappings[j];
                        }
                        m_TaskInfo[i] = m_TaskInfo[i].Substring(MappingProvider.Mappings[j].Identifier.Length);
                        break;
                    }
                }
                if (currentMap == null)
                    name.Append(" " + m_TaskInfo[i]);
                else
                    currentProperty.Append(" " + m_TaskInfo[i]);
            }
            if (currentMap != null)
                currentMap.Visit(t, currentProperty.ToString().Trim());
            t.Name = name.ToString().Trim();
            return t;
        }
    }
}
