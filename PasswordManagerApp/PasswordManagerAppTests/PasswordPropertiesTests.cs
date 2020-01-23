using Moq;
using NUnit.Framework;
using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerAppTests
{
    public class PasswordPropertiesTests
    {
        private PasswordManagerUser user;
        private PasswordProperties password;
        private const string USER_NAME = "mock user";
        private const string USER_PASSWORD = "mock password";
        
        [SetUp]
        public void Setup()
        {
            user = PasswordManagerUser.Factory.CreateNewUser(USER_NAME, USER_PASSWORD);
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

            string generatedPassword = password.GeneratePassword(USER_PASSWORD);

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

        [Test]
        public void ChangeThisPassword_PasswordIsUnique()
        {
            password.Length = 5;
            string originalPassword = password.GeneratePassword(USER_PASSWORD);
            password.ChangeThisPassword();
            string newPassword = password.GeneratePassword(USER_PASSWORD);

            Assert.AreNotEqual(originalPassword, newPassword);
        }

        [Test]
        public void ChangePasswordProperty_PasswordChanges()
        {
            password.Name = "Google";
            password.Length = 8;
            List<string> passwordList = new List<string>() { password.GeneratePassword(USER_PASSWORD) };
            foreach(PropertyInfo pi in password.GetType().GetProperties().Where(p => p.PropertyType == typeof(bool)))
            {
                bool oldValue = (bool)pi.GetValue(password);
                pi.SetValue(password, !oldValue);
                passwordList.Add(password.GeneratePassword(USER_PASSWORD));
            }
            password.Name = "Yahoo";
            passwordList.Add(password.GeneratePassword(USER_PASSWORD));
            password.Length = 7;
            passwordList.Add(password.GeneratePassword(USER_PASSWORD));

            CollectionAssert.AllItemsAreUnique(passwordList);
        }
    }
}
