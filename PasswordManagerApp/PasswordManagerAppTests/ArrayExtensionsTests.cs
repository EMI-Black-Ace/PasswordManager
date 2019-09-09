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
    }
}
