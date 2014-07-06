using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using NUnit.Framework;
using s7.cmDo.ParameterMaps;

namespace s7.cmDo.Tests.ParameterMaps
{
    public abstract class ParameterMapTestBase<T> where T : IParameterToFieldMap, new()
    {
        public Task TestTask;
        public T Map;
        public virtual string TestString { get { return "TestValue"; } }

        [SetUp]
        public void setup()
        {
            TestTask = new Task();
            Map = new T();
        }
    }
}
