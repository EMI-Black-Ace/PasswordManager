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
    public class PasswordManagerUserTests
    {
        private const string testHashFile = "../../../Resources/passwordHash.json";

        private class PasswordClass
        {
            public string TestPassword { get; set; } = "Worst Password Ever!";
            public byte[] Hash { get; set; }
        }

        private PasswordClass Password { get; set; }

        private PasswordManagerUser passwordManagerUser;

        [OneTimeSetUp]
        public void StartTests()
        {
            if (!File.Exists(testHashFile))
            {
                Password = new PasswordClass();
                SHA1 hashGenerator = new SHA1CryptoServiceProvider();
                Password.Hash = hashGenerator.ComputeHash(Encoding.ASCII.GetBytes(Password.TestPassword));
                string persistentPassword = JsonConvert.SerializeObject(Password);
                File.WriteAllText(testHashFile, persistentPassword);
            }
            else
            {
                string jsonPassword = File.ReadAllText(testHashFile);
                Password = JsonConvert.DeserializeObject<PasswordClass>(jsonPassword);
            }
            
        }

        [SetUp]
        public void Setup()
        {
            passwordManagerUser = new PasswordManagerUser("Test", Password.TestPassword, new SHA1CryptoServiceProvider());
        }

        /// <summary>
        /// Demonstrates that the generated password hash is the same every time.
        /// </summary>
        [Test]
        public void HashConsistencyTest()
        {
            Assert.AreEqual(Password.Hash, passwordManagerUser.PasswordHash);
        }

        [Test]
        public void CheckPassword_PasswordIsValid_ReturnsTrue()
        {
            Assert.IsTrue(passwordManagerUser.CheckPassword(Password.TestPassword));
        }

        [Test]
        public void CheckPassword_PasswordIsInvalid_ReturnsFalse()
        {
            Assert.IsFalse(passwordManagerUser.CheckPassword(Password.TestPassword + "_Invalid"));
        }
    }
}
