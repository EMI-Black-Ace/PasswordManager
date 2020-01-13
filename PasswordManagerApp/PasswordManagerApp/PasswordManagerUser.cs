using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerApp
{
    public class PasswordManagerUser
    {
        public string Name { get; set; }
        private List<PasswordProperties> passwords = new List<PasswordProperties>();
        public IReadOnlyCollection<PasswordProperties> Passwords => passwords;
        public byte[] PasswordHash { get; set; }
        public HashAlgorithm HashAlgorithm { get; set; }

        public bool CheckPassword(string password)
        {
            byte[] hashResult = HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            return hashResult.AllItemsAreEqual(PasswordHash);
        }

        public PasswordProperties CreateNewStoredPassword()
        {
            var newPassword = new PasswordProperties(this);
            passwords.Add(newPassword);
            return newPassword;
        }

        public void RemoveStoredPassword(PasswordProperties password)
        {
            passwords.Remove(password);
        }

        internal PasswordManagerUser(string name, string masterPassword, HashAlgorithm hashAlgorithm)
        {
            Name = name;
            HashAlgorithm = hashAlgorithm;
            PasswordHash = HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(masterPassword));
        }

        public static PasswordManagerUserFactory Factory = new PasswordManagerUserFactory();
    }
}
