using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerAppTests
{
    public class PasswordManagerUserTests
    {
        private const string testHashFile = "../../Resources/passwordHash.txt";

        private class PasswordClass
        {
            public string testPassword { get; set; } = "Worst Password Ever!";
            public byte[] hash { get; set; }
        }
        private PasswordClass password { get; set; }

        [OneTimeSetUp]
        public void StartTests()
        {
            if (!File.Exists(testHashFile))
            {
                SHA1 hashGenerator = new SHA1CryptoServiceProvider();
                var hash = hashGenerator.ComputeHash(Encoding.ASCII.GetBytes(password.testPassword));
                using (Stream sw = File.Open(testHashFile, FileMode.CreateNew))
                {
                    sw.Write(BitConverter.GetBytes(testPassword.Length));
                    sw.Write(testPassword.Cast<byte>().ToArray());
                    sw.Write(hash);
                }
            }
            else
            {
                using(Stream sr = File.Open(testHashFile, FileMode.Open))
                {
                    byte[] lengthRead = new byte[sizeof(int)];
                    sr.Read(lengthRead, 0, sizeof(int));
                    int length = BitConverter.ToInt32(lengthRead);
                    byte[] passwordRead = new byte[length];
                    sr.Read(passwordRead, 0, length);
                    sr.
                }
            }
            
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void HashConsistencyTest()
        {

        }
    }
}
