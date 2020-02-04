using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCommonLibrary
{
    public class TestPasswordManagerUsers
    {
        private static TestPasswordManagerUsers _instance;
        public static TestPasswordManagerUsers Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TestPasswordManagerUsers();
                return _instance;
            }
        }

        private TestPasswordManagerUsers()
        {
            users = GenerateUsers().Take(32).ToList();
        }

        private List<PasswordManagerUser> users;
        public IReadOnlyCollection<PasswordManagerUser> Users => users;

        private IEnumerable<PasswordManagerUser> GenerateUsers()
        {
            int usersGenerated = 0;
            var rng = new Random();
            while (true)
            {
                var buffer = new byte[rng.Next(1, 32)];
                rng.NextBytes(buffer);
                var password = new string(buffer.Select(b => CharacterTables.printableTable.WrapSelectChar(b)).ToArray());
                var user = PasswordManagerUser.Factory.CreateNewUser($"User{usersGenerated}",
                    $"{password}");
                for(int passwordsGenerated = 0; passwordsGenerated < rng.Next(1, 32); ++passwordsGenerated)
                {
                    var newPassword = user.CreateNewStoredPassword();
                    newPassword.Length = rng.Next(5, 32);
                    newPassword.MustHaveCaps = rng.Next(0, 1) == 0;
                    newPassword.MustHaveLower = rng.Next(0, 1) == 0;
                    newPassword.MustHaveNumber = rng.Next(0, 1) == 0;
                    newPassword.MustHaveSpc = rng.Next(0, 1) == 0;
                    newPassword.MustNotHaveSpc = rng.Next(0, 1) == 0;
                    newPassword.Name = $"Password{passwordsGenerated}";
                }

                yield return user;
            }
        }
    }
}
