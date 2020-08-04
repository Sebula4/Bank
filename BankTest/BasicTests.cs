using BankLibrary;
using System;
using Xunit;

namespace BankTest
{
    public class BasicTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void Test2()
        {
            var account1 = new BankAccount("Seb", 1000.50M);

            //Test for a negative balance.
            Assert.Throws<InvalidOperationException>(
                () => account1.MakeWithdrawal(500000, DateTime.Now, "Attempt to overdraw")
            );
        }

        [Fact]
        public void Test3()
        {
            // Test that the initial balances must be positive.
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new BankAccount("Invalid", -55)
            );
        }
    }
}
