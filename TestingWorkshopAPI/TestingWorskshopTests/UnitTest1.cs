using System;
using Xunit;

namespace TestingWorskshopTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange

            //act

            //assert
        }

        [Theory]
        [InlineData(1)] //arrange
        public void TheoryTest1(int a)
        {
            Assert.True(a == 1);
        }
    }
}
