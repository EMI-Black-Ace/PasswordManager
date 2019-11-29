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
        public ICollection<PasswordProperties> Passwords { get; set; } = new HashSet<PasswordProperties>();
        public byte[] PasswordHash { get; set; }
        public HashAlgorithm HashAlgorithm { get; set; }

        public bool CheckPassword(string password)
        {
            byte[] hashResult = HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            return hashResult.AllItemsAreEqual(PasswordHash);
        }

        public PasswordManagerUser(IUserProvider provider, HashAlgorithm hashAlgorithm)
        {
            Name = provider.UserName;
            HashAlgorithm = hashAlgorithm;
            PasswordHash = HashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(provider.MasterPassword));
        }
    }
}
