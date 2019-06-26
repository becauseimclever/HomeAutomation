using FsCheck;
using System;
using System.Linq;
using Xunit;

namespace HomeAutomationPropertiesTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Prop.ForAll<int[]>(xs => xs.Reverse().Reverse().SequenceEqual(xs)).QuickCheckThrowOnFailure();
        }
    }
}
