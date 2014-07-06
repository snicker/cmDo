using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using s7.cmDo.ParameterMaps;

namespace s7.cmDo
{
    [Serializable]
    public class DefaultTask
    {
        public string Context { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DueTime { get; set; }
        public string Folder { get; set; }
        public string Goal { get; set; }
        public int Length { get; set; }
        public string Note { get; set; }
        public string Priority { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string Tag { get; set; }

        public Task ToTask()
        {
            Task t = new Task()
            {
                Length = this.Length,
                Note = this.Note,
                Start = this.StartDate,
                Tag = this.Tag,
            };
            t.Due = this.DueDate == DateTime.MinValue ? this.DueTime : this.DueDate;
            if (Context != null)    new ContextMap().Visit(t, Context);
            if (Folder != null)     new FolderMap().Visit(t, Folder);
            if (Goal != null)       new GoalMap().Visit(t, Goal);
            new PriorityMap().Visit(t, Priority);
            new StatusMap().Visit(t, Status);
            return t;
        }

        public static DefaultTask FromTask(Task t)
        {
            DefaultTask dt = new DefaultTask()
            {
                DueDate = t.Due,
                DueTime = t.Due,
                Length = t.Length,
                Note = t.Note,
                Priority = t.Priority.ToString(),
                StartDate = t.Start,
                Status = t.Status.ToString(),
                Tag = t.Tag,
            };
            dt.Context = t.Context != null ? t.Context.Name : null;
            dt.Folder = t.Folder != null ? t.Folder.Name : null;
            dt.Goal = t.Goal != null ? t.Goal.Name : null;
            return dt;
        }

    }
}
