using NUnit.Framework;
using System.Collections.Generic;
using PasswordManagerInterfaces;

namespace PasswordManagerAppTests
{
    public class PMALinqExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AllItemsAreEqual_EqualIntegerLists_ReturnsTrue()
        {
            List<int> ints1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> ints2 = new List<int>() { 1, 2, 3, 4, 5 };
            Assert.IsTrue(ints1.AllItemsAreEqual(ints2), "AllItemsAreEqual should have returned true, did not");
        }

        [Test]
        public void AllItemsAreEqual_OutOfOrderIntegerLists_ReturnsFalse()
        {
            List<int> ints1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> ints2 = new List<int>() { 5, 4, 3, 2, 1 };
            Assert.IsFalse(ints1.AllItemsAreEqual(ints2), "AllItemsAreEqual should have returned false, did not");
        }

        [Test]
        public void AllItemsAreEqual_ListLengthNotSame_ReturnsFalse()
        {
            List<int> ints1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> ints2 = new List<int>() { 1, 2, 3, 4 };
            Assert.IsFalse(ints1.AllItemsAreEqual(ints2), "AllItemsAreEqual should have returned false, did not");
        }
    }
}