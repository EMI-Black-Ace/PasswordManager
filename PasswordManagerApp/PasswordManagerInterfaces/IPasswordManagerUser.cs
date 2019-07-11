using System;
using System.Collections.Generic;

namespace PasswordManagerInterfaces
{
    public interface IPasswordManagerUser
    {
        string Name { get; }
        ICollection<IPasswordProperties> Passwords { get; }

        /// <summary>
        /// Returns true if the hash created by this password matches the interal hash.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckPassword(string password);
    }
}
