using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using PasswordManagerApp;

namespace PasswordManagerAppTests
{
    public class ArrayExtensionsTests
    {
        [Test]
        public void AllItemsAreEqual_EqualItems_ReturnsTrue()
        {
            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 1, 2, 3 };
            Assert.IsTrue(array1.AllItemsAreEqual(array2));
            Assert.IsTrue(array2.AllItemsAreEqual(array1));
        }

        [Test]
        public void AllItemsAreEqual_DifferentLengths_ReturnsFalse()
        {
            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 1, 2, 3, 4 };
            Assert.IsFalse(array1.AllItemsAreEqual(array2));
            Assert.IsFalse(array2.AllItemsAreEqual(array1));
        }

        [Test]
        public void AllItemsAreEqual_DifferentItems_ReturnsFalse()
        {
            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 1, 2, 4 };
            Assert.IsFalse(array1.AllItemsAreEqual(array2));
            Assert.IsFalse(array2.AllItemsAreEqual(array1));
        }

        [Test]
        public void ReduceOrExpand_EqualLength_ReturnsCopyOfOriginal()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int[] newArray = array.ReduceOrExpand(5, (x, y) => x + y);
            Assert.IsTrue(array.AllItemsAreEqual(newArray));
        }

        [Test]
        public void ReduceOrExpand_Larger_LengthMatchesSpecAndItemsFilled()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int[] newArray = array.ReduceOrExpand(20, (x, y) => x + y);
            Assert.AreEqual(20, newArray.Length);
            Assert.IsFalse(array.Any((x) => x == 0));
        }

        [Test]
        public void ReduceOrExpand_Smaller_LengthMatchesSpecAndItemsUnique()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            int[] newArray = array.ReduceOrExpand(5, (x, y) => x + y);
            Assert.AreEqual(5, newArray.Length);
            CollectionAssert.IsNotSubsetOf(newArray, array);
        }

        [Test]
        public void ReduceOrExpand_ZeroLength_ThrowsException()
        {
            int[] array = { 1, 2, 3 };
            Assert.Throws<ArgumentException>(() => array.ReduceOrExpand(0, (x, y) => x + y));
        }
    }
}
