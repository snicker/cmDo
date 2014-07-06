using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using s7.cmDo;

namespace s7.cmDo.Tests
{
    [TestFixture]
    public class DefaultTaskTest
    {
        public const string TaskName = "Test Task";
        public const string TaskFolder = "TFolder";
        public const string TaskContext = "TContext";
        public const string TaskPriority = "High";
        public const string TaskStartDate = "7/7/2009";
        public const string TaskEndDate = "7/9/2009";
        public const string TaskStatus = "NextAction";
        public const string TaskNote = "This note has been duly noted.";
        public const string TaskGoal = "Build a world empire of tasks";

        private Toodledo.Model.Task TestTask()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} *{1} @{2} !{3} >{4} #{5} ${6} ++{7} +{8}", TaskName, TaskFolder, TaskContext, TaskPriority, TaskStartDate, TaskEndDate, TaskStatus, TaskNote, TaskGoal));
            return tb.ToTask();
        }

        private DefaultTask TestDefaultTask()
        {
            DefaultTask t = new DefaultTask()
            {
                Folder = TaskFolder,
                Context = TaskContext,
                Priority = TaskPriority,
                Status = TaskStatus,
                Note = TaskNote,
                Goal = TaskGoal,
            };
            DateTime duetime;
            DateParser.TryParse(TaskEndDate, out duetime);
            t.DueDate = duetime.Date;
            DateTime starttime;
            DateParser.TryParse(TaskStartDate, out starttime);
            t.StartDate = starttime.Date;
            return t;
        }

        [Test]
        public void test_create_default_task_from_task_are_equal()
        {
            Toodledo.Model.Task t = TestTask();
            DefaultTask dt = DefaultTask.FromTask(t);
            assert_task_and_default_are_equal(t, dt);
        }

        [Test]
        public void test_create_task_from_default_task_are_equal()
        {
            DefaultTask dt = TestDefaultTask();
            Toodledo.Model.Task t = dt.ToTask();
            assert_task_and_default_are_equal(t, dt);
        }

        [Test]
        public void test_round_trip_default_task_to_task_and_back()
        {
            DefaultTask dt = TestDefaultTask();
            Toodledo.Model.Task t = dt.ToTask();
            DefaultTask dt2 = DefaultTask.FromTask(t);
            Assert.AreEqual(dt2.Context, dt.Context);
            Assert.AreEqual(dt2.DueDate, dt.DueDate);
            Assert.AreEqual(dt2.Folder, dt.Folder);
            Assert.AreEqual(dt2.Goal, dt.Goal);
            Assert.AreEqual(dt2.Length, dt.Length);
            Assert.AreEqual(dt2.Note, dt.Note);
            Assert.AreEqual(dt2.Priority, dt.Priority);
            Assert.AreEqual(dt2.StartDate, dt.StartDate);
            Assert.AreEqual(dt2.Status, dt.Status);
            Assert.AreEqual(dt2.Tag, dt.Tag);
        }

        [Test]
        public void test_round_trip_task_to_default_task_and_back()
        {
            Toodledo.Model.Task t = TestTask();
            DefaultTask dt = DefaultTask.FromTask(t);
            Toodledo.Model.Task t2 = dt.ToTask();
            Assert.AreEqual(t2.Context.Name, t.Context.Name);
            Assert.AreEqual(t2.Due, t.Due);
            Assert.AreEqual(t2.Folder, t.Folder);
            Assert.AreEqual(t2.Goal, t.Goal);
            Assert.AreEqual(t2.Length, t.Length);
            Assert.AreEqual(t2.Note, t.Note);
            Assert.AreEqual(t2.Priority, t.Priority);
            Assert.AreEqual(t2.Start, t.Start);
            Assert.AreEqual(t2.Status, t.Status);
            Assert.AreEqual(t2.Tag, t.Tag);
        }

        private void assert_task_and_default_are_equal(Toodledo.Model.Task t, DefaultTask dt)
        {
            Assert.AreEqual(t.Context.Name, dt.Context);
            Assert.AreEqual(t.Due.Date, dt.DueDate);
            Assert.AreEqual(t.Folder.Name, dt.Folder);
            Assert.AreEqual(t.Goal.Name, dt.Goal);
            Assert.AreEqual(t.Length, dt.Length);
            Assert.AreEqual(t.Note, dt.Note);
            Assert.AreEqual(t.Priority.ToString(), dt.Priority);
            Assert.AreEqual(t.Start.Date, dt.StartDate.Date);
            Assert.AreEqual(t.Status.ToString(), dt.Status);
            Assert.AreEqual(t.Tag, dt.Tag);
        }
    }
}
