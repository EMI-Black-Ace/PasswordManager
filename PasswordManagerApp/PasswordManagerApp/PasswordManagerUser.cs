using PasswordManagerInterfaces;
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
        public ICollection<IPasswordProperties> Passwords { get; set; }
        public byte[] PasswordHash { get; set; }
        private HashAlgorithm hashAlgorithm;
        public bool CheckPassword(string password)
        {
            byte[] hashResult = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
            return hashResult.Zip(PasswordHash, (r, p) => r == p).All(x => x);
        }
    }
}
