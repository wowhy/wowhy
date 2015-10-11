using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    using HY.Utitily.ExtensionMethod;

    [TestClass]
    public class FlagExtensionTest
    {
        [Flags]
        enum Pets
        {
            None = 0,
            Dog = 0x01,
            Cat = 0x02,
            Fish = 0x04
        }

        private Pets[] pets = new Pets[]
            {
                Pets.Cat,
                Pets.Cat | Pets.Dog,
                Pets.Dog,
                Pets.Dog | Pets.Fish,
                Pets.Cat | Pets.Fish
            };

        [TestMethod]
        public void TestMethod1()
        {
            var type = typeof(Pets);

            Assert.IsTrue(pets[0].IsSet(Pets.Cat));
            Assert.IsTrue(pets[1].IsSet(Pets.Cat));
            Assert.IsTrue(!pets[2].IsSet(Pets.Cat));
            Assert.IsTrue(!pets[3].IsSet(Pets.Cat));
            Assert.IsTrue(pets[4].IsSet(Pets.Cat));

            Assert.IsTrue(type != null);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsFalse(pets[0].IsClear(Pets.Cat));
            Assert.IsFalse(pets[1].IsClear(Pets.Cat));
            Assert.IsFalse(!pets[2].IsClear(Pets.Cat));
            Assert.IsFalse(!pets[3].IsClear(Pets.Cat));
            Assert.IsFalse(pets[4].IsClear(Pets.Cat));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsTrue(pets[1].AnyFlagsSet(Pets.Cat | Pets.Dog));
            Assert.IsTrue(pets[1].AnyFlagsSet(Pets.Dog | Pets.Fish));
        }
    }
}
