using Moq;
using NUnit.Framework;
using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerAppTests
{
    public class PasswordPropertiesTests
    {
        private PasswordManagerUser user;
        private PasswordProperties password;
        private const string userName = "mock user";
        private const string userPassword = "mock password";
        
        [SetUp]
        public void Setup()
        {
            user = PasswordManagerUser.Factory.CreateNewUser(userName, userPassword);
            password = user.CreateNewStoredPassword();
        }

        [Test]
        public void PasswordAttributes_GeneratedPasswordsMeetCriteria(
            [Values("Google", "Yahoo", "Pneumonoultramicroscopicsiliconvolcaniosis")] string name,
            [Range(5,50)] int length,
            [Values(true, false)] bool mustHaveCaps,
            [Values(true, false)] bool mustHaveLower,
            [Values(true, false)] bool mustHaveNumber,
            [Values(true, false)] bool mustHaveSpecial,
            [Values(true, false)] bool mustNotHaveSpecial
            )
        {
            password.Name = name;
            password.Length = length;
            password.MustHaveCaps = mustHaveCaps;
            password.MustHaveLower = mustHaveLower;
            password.MustHaveNumber = mustHaveNumber;
            password.MustHaveSpc = mustHaveSpecial;
            password.MustNotHaveSpc = mustNotHaveSpecial;

            string generatedPassword = password.GeneratePassword(userPassword);

            Assert.AreEqual(password.Length, generatedPassword.Length);

            if (password.MustHaveCaps)
            {
                Assert.IsTrue(generatedPassword.Any(c => CharacterTables.upperTable.Contains(c))); 
            }
            if (password.MustHaveLower)
            {
                Assert.IsTrue(generatedPassword.Any(c => CharacterTables.lowerTable.Contains(c)));
            }
            if (password.MustHaveNumber)
            {
                Assert.IsTrue(generatedPassword.Any(c => CharacterTables.numberTable.Contains(c)));
            }
            if (password.MustHaveSpc)
            {
                Assert.IsTrue(generatedPassword.Any(c => CharacterTables.spcTable.Contains(c)));
            }
            if (password.MustNotHaveSpc)
            {
                Assert.IsFalse(generatedPassword.Any(c => CharacterTables.spcTable.Contains(c)));
            }
        }

    }
}
