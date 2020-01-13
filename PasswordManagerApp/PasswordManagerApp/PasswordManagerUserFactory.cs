using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerApp
{
    public class PasswordManagerUserFactory
    {
        private readonly HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider();
        public PasswordManagerUser CreateNewUser(string name, string masterPassword)
        {
            return new PasswordManagerUser(name, masterPassword, hashAlgorithm);
        }
    }
}
