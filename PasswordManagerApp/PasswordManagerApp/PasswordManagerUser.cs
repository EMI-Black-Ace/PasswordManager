using PasswordManagerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerApp
{
    public class PasswordManagerUser : IPasswordManagerUser
    {
        public string Name { get; set; }
        public ICollection<IPasswordProperties> Passwords { get; set; }
        public byte[] PasswordHash { get; set; }
        private HashAlgorithm hashAlgorithm;

        public bool CheckPassword(string password)
        {
            byte[] hashResult = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            return hashResult.AllItemsAreEqual(PasswordHash);
        }

        public PasswordManagerUser(string name, string masterPassword, HashAlgorithm hashAlgorithm)
        {
            Name = name;
            this.hashAlgorithm = hashAlgorithm;
            PasswordHash = this.hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(masterPassword));
        }
    }
}
