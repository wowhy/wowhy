namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using HY.Utitily.Lambda;

    [TestClass]
    public class LambdaTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fab = Recursion.R<int, int>((self, n) => n <= 2 ? 1 : self(n - 1) + self(n - 2));
            var nums = new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
            for (int i = 1; i <= 10; i++)
            {
                Assert.AreEqual(fab(i), nums[i - 1]);
            }
        }
    }
}