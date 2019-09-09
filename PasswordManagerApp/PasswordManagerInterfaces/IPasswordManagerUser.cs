using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PasswordManagerInterfaces
{
    public interface IPasswordManagerUser
    {
        string Name { get; }
        ICollection<IPasswordProperties> Passwords { get; }
        byte[] PasswordHash { get; }
        HashAlgorithm HashAlgorithm { get; }

        /// <summary>
        /// Returns true if the hash created by this password matches the interal hash.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckPassword(string password);
    }
}
