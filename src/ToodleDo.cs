using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using Toodledo.Model.API;
using Toodledo.Client;

namespace s7.cmDo
{
    public class ToodleDo
    {
        private static Session m_Session;

        private static Session Session
        {
            get { return m_Session; }
        }

        private static IGeneral General
        {
            get { return (IGeneral)Session; }
        }

        private static ITasks Tasks
        {
            get { return (ITasks)Session; }
        }

        private static IList<Context> m_Contexts;
        private static IList<Folder> m_Folders;
        private static IList<Goal> m_Goals;

        public static IList<Context> Contexts
        {
            get
            {
                if (m_Contexts == null || ( m_Contexts != null && m_Contexts.Count == 0 ) )
                    LoadContexts();
                return m_Contexts;
            }
        }

        public static IList<Folder> Folders
        {
            get
            {
                if (m_Folders == null || (m_Folders != null && m_Folders.Count == 0))
                    LoadFolders();
                return m_Folders;
            }
        }

        public static IList<Goal> Goals
        {
            get
            {
                if (m_Goals == null || (m_Goals != null && m_Goals.Count == 0))
                    LoadGoals();
                return m_Goals;
            }
        }

        public static void Connect(string username, string password) {
            m_Session = Session.Create(username, password, "cmDo");
        }

        private static void LoadContexts()
        {
            if(m_Session != null)
                m_Contexts = ((IGeneral)Session).GetContexts().ToList<Context>();
            else
                m_Contexts = new List<Context>();
        }

        private static void LoadFolders()
        {
            if(m_Session != null)
                m_Folders = ((IGeneral)Session).GetFolders().ToList<Folder>();
            else
                m_Folders = new List<Folder>();
        }

        private static void LoadGoals()
        {
            if (m_Session != null)
                m_Goals = ((IGeneral)Session).GetGoals().ToList<Goal>();
            else
                m_Goals = new List<Goal>();
        }

        public static Folder AddFolder(string title, bool isPrivate)
        {
            Folder f = new Folder() { Name = title, IsPrivate = isPrivate };
            if (m_Session != null)
                f.Id = General.AddFolder(f.Name, f.IsPrivate);
            Folders.Add(f);
            return f;
        }

        public static Context AddContext(string title)
        {
            Context c = new Context() { Name = title };
            if (m_Session != null)
                c.Id = General.AddContext(c.Name);
            Contexts.Add(c);
            return c;
        }

        public static Goal AddGoal(string title, Level level, int contributes)
        {
            Goal g = new Goal() { Name = title, Level = level, Contributes = contributes };
            if (m_Session != null)
                g.Id = General.AddGoal(g.Name, g.Level, g.Contributes);
            Goals.Add(g);
            return g;
        }

        public static Task AddTask(Task t)
        {
            if (m_Session != null)
                t.Id = Tasks.AddTask(t);
            return t;
        }

    }
}
