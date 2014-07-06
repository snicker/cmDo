using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace s7.cmDo.Tests
{
    [TestFixture]
    public class TaskBuilderTest
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

        [Test]
        public void test_with_just_a_name()
        {
            TaskBuilder taskBuilder = new TaskBuilder(TaskName);
            Toodledo.Model.Task task = taskBuilder.ToTask();
            Assert.AreEqual(TaskName, task.Name);
        }

        [Test]
        public void test_with_just_a_folder()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} *{1}", TaskName, TaskFolder));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.NotNull(task.Folder);
            Assert.AreEqual(TaskFolder, task.Folder.Name);
        }

        [Test]
        public void test_with_just_a_context()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} @{1}", TaskName, TaskContext));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.NotNull(task.Context);
            Assert.AreEqual(TaskContext, task.Context.Name);
        }

        [Test]
        public void test_with_just_priority()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} !{1}", TaskName, TaskPriority));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(Toodledo.Model.Priority.High, task.Priority);
        }

        [Test]
        public void test_with_just_status()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} ${1}", TaskName, TaskStatus));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(Toodledo.Model.Status.NextAction, task.Status);
        }

        [Test]
        public void test_with_just_a_note()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} ++{1}", TaskName, TaskNote));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(TaskNote, task.Note);
        }

        [Test]
        public void test_with_just_a_startdate()
        {
            DateTime time;
            DateParser.TryParse(TaskStartDate, out time);
            TaskBuilder tb = new TaskBuilder(string.Format("{0} >{1}", TaskName, TaskStartDate));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(time.Date, task.Start.Date);
        }

        [Test]
        public void test_with_just_a_duedate()
        {
            DateTime time;
            DateParser.TryParse(TaskEndDate, out time);
            TaskBuilder tb = new TaskBuilder(string.Format("{0} #{1}", TaskName, TaskEndDate));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(time.Date, task.Due.Date);
        }

        [Test]
        public void test_due_date_from_name()
        {
            DateTime tomorrow = DateTime.Now + TimeSpan.FromDays(1);
            TaskBuilder tb = new TaskBuilder(string.Format("{0} {1}", TaskName, "tomorrow"));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(tomorrow.Date, task.Due.Date);
        }

        [Test]
        public void test_waiting_for_status_inferred_from_name()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} {1}", TaskName, "WF"));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(Toodledo.Model.Status.Waiting, task.Status);
            tb = new TaskBuilder(string.Format("{0} {1}", TaskName, "waiting on"));
            task = tb.ToTask();
            Assert.AreEqual(Toodledo.Model.Status.Waiting, task.Status);
            tb = new TaskBuilder(string.Format("{0} {1}", TaskName, "waiting for"));
            task = tb.ToTask();
            Assert.AreEqual(Toodledo.Model.Status.Waiting, task.Status);
        }

        [Test]
        public void test_waiting_in_name_but_not_in_status()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} {1} ${2}", TaskName, "waiting", TaskStatus));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(TaskStatus, task.Status.ToString());
            Assert.AreNotEqual(Toodledo.Model.Status.Waiting, task.Status);
        }

        [Test]
        public void test_goal()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} +{1}", TaskName, TaskGoal));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.NotNull(task.Goal);
            Assert.AreEqual(TaskGoal, task.Goal.Name);
        }

        [Test]
        public void test_just_about_everything()
        {
            TaskBuilder tb = new TaskBuilder(string.Format("{0} *{1} @{2} !{3} >{4} #{5} ${6} ++{7}", TaskName, TaskFolder, TaskContext, TaskPriority, TaskStartDate, TaskEndDate, TaskStatus, TaskNote));
            Toodledo.Model.Task task = tb.ToTask();
            Assert.AreEqual(TaskName, task.Name);
            Assert.NotNull(task.Folder);
            Assert.AreEqual(TaskFolder, task.Folder.Name);
            Assert.NotNull(task.Context);
            Assert.AreEqual(TaskContext, task.Context.Name);
            Assert.AreEqual(Toodledo.Model.Priority.High, task.Priority);
            Assert.AreEqual(Toodledo.Model.Status.NextAction, task.Status);
            Assert.AreEqual(TaskNote, task.Note);
            DateTime duetime;
            DateParser.TryParse(TaskEndDate, out duetime);
            Assert.AreEqual(duetime.Date, task.Due.Date);
            DateTime starttime;
            DateParser.TryParse(TaskStartDate, out starttime);
            Assert.AreEqual(starttime.Date, task.Start.Date);
        }
    }
}
