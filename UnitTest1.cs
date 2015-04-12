using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ProgressiveTest
{
    [TestClass]
    public class UnitTest1 : CS.TestUtils.ProgressiveTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            this.Progress.Open("Running " + this.GetType().Name);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.Progress.Close();
        }

        [TestMethod]
        public void TestMethod1()
        {
            this.Progress.Message = "Randomizing data...";

            const int MAX = 100000;

            var rng = new Random();

            var arr = Enumerable
                .Repeat(0, MAX)
                .Select((v, i) =>
                {
                    this.Progress.Progress = (int)((i + 1) * 100M / (decimal)MAX);

                    return rng.Next();
                })
                .ToArray();

            this.Progress.Message = "Incrementing data...";

            arr = arr
                .Select((v, i) =>
                {
                    this.Progress.Progress = (int)((i + 1) * 100M / (decimal)MAX);

                    return v + 1;
                })
                .ToArray();

            this.Progress.Message = "Test complete.";
        }
    }
}
