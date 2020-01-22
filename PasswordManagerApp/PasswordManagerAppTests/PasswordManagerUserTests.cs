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
            public string MasterPassword { get; set; } = "Worst Password Ever!";
            public byte[] Hash { get; set; }

            public string UserName => "Test";
        }

        private static PasswordClass UserProvider { get; set; }

        private PasswordManagerUser passwordManagerUser;

        [OneTimeSetUp]
        public void StartTests()
        {
            if (!File.Exists(testHashFile))
            {
                UserProvider = new PasswordClass();
                SHA1 hashGenerator = new SHA1CryptoServiceProvider();
                UserProvider.Hash = hashGenerator.ComputeHash(Encoding.ASCII.GetBytes(UserProvider.MasterPassword));
                string persistentPassword = JsonConvert.SerializeObject(UserProvider);
                File.WriteAllText(testHashFile, persistentPassword);
            }
            else
            {
                string jsonPassword = File.ReadAllText(testHashFile);
                UserProvider = JsonConvert.DeserializeObject<PasswordClass>(jsonPassword);
            }
            
        }

        [SetUp]
        public void Setup()
        {
            passwordManagerUser = PasswordManagerUser.Factory.CreateNewUser(UserProvider.UserName, UserProvider.MasterPassword);
        }

        /// <summary>
        /// Demonstrates that the generated password hash is the same every time.
        /// </summary>
        [Test]
        public void HashConsistencyTest()
        {
            Assert.AreEqual(UserProvider.Hash, passwordManagerUser.PasswordHash);
        }

        [Test]
        public void CheckPassword_PasswordIsValid_ReturnsTrue()
        {
            Assert.IsTrue(passwordManagerUser.CheckPassword(UserProvider.MasterPassword));
        }

        [Test]
        public void CheckPassword_PasswordIsInvalid_ReturnsFalse()
        {
            Assert.IsFalse(passwordManagerUser.CheckPassword(UserProvider.MasterPassword + "_Invalid"));
        }
    }
}
