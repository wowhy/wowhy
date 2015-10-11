using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HY.Utitily.Reflection;
using HY.Utitily.ExtensionMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    public class A
    {
        public int Id { get; set; }
        public string Name;
    }

    public struct B
    {
        public int Id { get; set; }
        public string Name;
    }

    [TestClass]
    public class ReflectionHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance(typeof(A)));
            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance<A>());

            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance<B>());
            Assert.IsNotNull(ReflectionHelper.Instance.FastCreateInstance(typeof(B)));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var list = new List<A>();
            var count = 10000;
            var type = typeof(A);
            var setter = type.GetProperySetter("Id");
            for (int i = 0; i < count; i++)
            {
                list.Add(type.FastCreateInstance<A>());
                setter(list[i], i);
            }

            Assert.IsTrue(list.Count == count);

            for (int i = 0; i < count; i++) 
            {
                Assert.IsTrue(i == list[i].Id);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var list = new List<A>();
            var count = 10000;
            var type = typeof(A);
            var setter = type.GetFieldSetter("Name");
            for (int i = 0; i < count; i++)
            {
                var a = type.FastCreateInstance<A>();
                setter(a, (i.ToString()));
                list.Add(a);
            }

            Assert.IsTrue(list.Count == count);

            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(i.ToString() == list[i].Name);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            var list = new List<B>();
            var count = 10000;
            var type = typeof(B);
            var setter = type.GetProperySetter("Id");
            for (int i = 0; i < count; i++)
            {
                B b = type.FastCreateInstance<B>();
                b = (B)setter(b, i);
                list.Add(b);
            }

            Assert.IsTrue(list.Count == count);

            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(i == list[i].Id);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            var list = new List<A>();
            var count = 10000;
            var type = typeof(A);
            var setter = type.GetFieldSetter("Name");
            for (int i = 0; i < count; i++)
            {
                var a = type.FastCreateInstance<A>();
                setter(a, (i.ToString()));
                list.Add(a);
            }

            Assert.IsTrue(list.Count == count);

            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(i.ToString() == list[i].Name);
            }
        }

        public class TestA
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public DateTime Time;
        }

        [TestMethod]
        public void TestMethod6()
        {
            var time = DateTime.Now;

            var accector = new FastDataAccector(typeof(TestA));
            accector["Id"] = 10;
            accector["Name"] = "Hello";
            accector["Time"] = time;

            Assert.AreEqual(accector["Id"], 10);
            Assert.AreEqual(accector["Name"], "Hello");
            Assert.AreEqual(accector["Time"], time);
        }
    }
}
