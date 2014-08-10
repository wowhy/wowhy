using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ReflectionHelperTest
    {
        public class A
        {
            public int Id { get; set; }
        }

        public struct B
        {
            public string Name;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance(typeof(A)));
            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance<A>());

            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance<B>());
            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance(typeof(B)));
        }
    }
}
