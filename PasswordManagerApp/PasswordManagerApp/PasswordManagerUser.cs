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
        public ICollection<PasswordProperties> Passwords { get; set; }
        public byte[] PasswordHash { get; set; }
        public HashAlgorithm HashAlgorithm { get; set; }

        public bool CheckPassword(string password)
        {
            byte[] hashResult = HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            return hashResult.AllItemsAreEqual(PasswordHash);
        }

        public PasswordManagerUser(string name, string masterPassword, HashAlgorithm hashAlgorithm)
        {
            Name = name;
            HashAlgorithm = hashAlgorithm;
            PasswordHash = HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(masterPassword));
        }
    }
}
